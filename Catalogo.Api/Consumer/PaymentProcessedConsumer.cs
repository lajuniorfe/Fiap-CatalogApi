using Azure.Messaging.ServiceBus;
using Catalogo.API.Events;
using Catalogo.AppService.Bibliotecas.Services;
using System.Text.Json;

namespace Catalogo.API.Consumers
{
    public class PaymentProcessedConsumer : BackgroundService
    {
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PaymentProcessedConsumer> _logger;

        public PaymentProcessedConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<PaymentProcessedConsumer> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            var connectionString = configuration["ServiceBusConnection"]
             ?? throw new InvalidOperationException("ServiceBusConnection não configurada.");

            var client = new ServiceBusClient(connectionString);

            _processor = client.CreateProcessor("payment-processed", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = 1
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += ProcessMessageAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await _processor.StartProcessingAsync(stoppingToken);

            _logger.LogInformation("Consumer da fila payment-processed iniciado.");

            try
            {
                await Task.Delay(
                    Timeout.Infinite,
                    stoppingToken);
            }
            catch (TaskCanceledException)
            {
                // Aplicação sendo encerrada
            }

        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            try
            {
                var json = args.Message.Body.ToString();

                _logger.LogInformation("Mensagem recebida da fila payment-processed: {Message}", json);

                var order = JsonSerializer.Deserialize<PaymentProcessedEvent>(json);

                if (order == null)
                {
                    _logger.LogWarning("Não foi possível desserializar PaymentProcessedEvent.");

                    await args.DeadLetterMessageAsync(
                        args.Message,
                        "Mensagem inválida",
                        "Não foi possível desserializar PaymentProcessedEvent.");

                    return;
                }

                using var scope =
                    _scopeFactory.CreateScope();

                var bibliotecaService =
                    scope.ServiceProvider
                        .GetRequiredService<IBibliotecaAppServices>();

                if (order.Status == "Aprovado")
                {
                    await bibliotecaService.AdquirirJogo(order);
                    await args.CompleteMessageAsync(args.Message);

                    _logger.LogInformation("Jogo incluido com sucesso.");
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao processar mensagem da fila payment-processed.");

                await args.AbandonMessageAsync(args.Message);
            }
        }

        private Task ProcessErrorAsync(
          ProcessErrorEventArgs args)
        {
            _logger.LogError(
                args.Exception,
                "Erro no Service Bus. Entity: {EntityPath}",
                args.EntityPath);

            return Task.CompletedTask;
        }

        public override async Task StopAsync(
           CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(
                cancellationToken);

            await _processor.DisposeAsync();

            await base.StopAsync(cancellationToken);
        }

    }
}
