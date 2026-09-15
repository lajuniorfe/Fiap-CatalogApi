using Catalogo.API.Events;
using Catalogo.AppService.Bibliotecas.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Catalogo.API.Consumers
{
    public class PaymentProcessedConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<PaymentProcessedConsumer> _logger;

        private IConnection? _connection;
        private IChannel? _channel;

        public PaymentProcessedConsumer(
            IConfiguration configuration,
            IServiceScopeFactory scopeFactory,
            ILogger<PaymentProcessedConsumer> logger)
        {
            _configuration = configuration;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var connectionString =
                 _configuration["RabbitMQConnection"]
                ?? throw new InvalidOperationException(
                    "RabbitMQConnection não configurada.");

            var factory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };

            _connection =
                await factory.CreateConnectionAsync(stoppingToken);

            _channel =
                await _connection.CreateChannelAsync(
                    cancellationToken: stoppingToken);

            await _channel.QueueDeclareAsync(
                queue: "payment-processed",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            _logger.LogInformation(
                "Consumer da fila payment-processed iniciado.");

            var consumer =
                new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                await ProcessMessageAsync(ea);
            };

            await _channel.BasicConsumeAsync(
                queue: "payment-processed",
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

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

        private async Task ProcessMessageAsync(
            BasicDeliverEventArgs args)
        {
            try
            {
                var json = Encoding.UTF8.GetString(
                    args.Body.ToArray());

                _logger.LogInformation(
                    "Mensagem recebida da fila payment-processed: {Message}",
                    json);

                var order =
                    JsonSerializer.Deserialize<PaymentProcessedEvent>(
                        json);

                if (order == null)
                {
                    _logger.LogWarning(
                        "Não foi possível desserializar PaymentProcessedEvent.");

                    await _channel!.BasicNackAsync(
                        deliveryTag: args.DeliveryTag,
                        multiple: false,
                        requeue: false);

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

                    _logger.LogInformation(
                        "Jogo incluído com sucesso.");
                }

                await _channel!.BasicAckAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao processar mensagem da fila payment-processed.");

                await _channel!.BasicNackAsync(
                    deliveryTag: args.DeliveryTag,
                    multiple: false,
                    requeue: true);
            }
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            if (_channel != null)
            {
                await _channel.CloseAsync(
                    cancellationToken);
            }

            if (_connection != null)
            {
                await _connection.CloseAsync(
                    cancellationToken);
            }

            await base.StopAsync(cancellationToken);
        }
    }
}