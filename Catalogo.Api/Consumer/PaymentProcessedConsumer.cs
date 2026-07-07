using Catalogo.API.Events;
using Catalogo.API.Messaging;
using Catalogo.AppService.Bibliotecas.Services;

namespace Catalogo.API.Consumers
{
    public class PaymentProcessedConsumer : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IServiceScopeFactory _scopeFactory;
        public PaymentProcessedConsumer(IMessageBus messageBus, IServiceScopeFactory scopeFactory)
        {
            _messageBus = messageBus;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync( CancellationToken stoppingToken)
        {
            await _messageBus.SubscribeAsync<PaymentProcessedEvent>("payment-processed", IncluirJogoBiblioteca);

            await Task.Delay(Timeout.Infinite, stoppingToken);

        }

        private async Task IncluirJogoBiblioteca(PaymentProcessedEvent ev)
        {
            using var scope = _scopeFactory.CreateScope();

            if (ev.Status == "Aprovado")
            {
                var bibliotecaService = scope.ServiceProvider.GetRequiredService<IBibliotecaAppServices>();

                await bibliotecaService.AdquirirJogo(ev);
            }
        }
    }
}
