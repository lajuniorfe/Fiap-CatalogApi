using Catalogo.API.Events;
using Catalogo.API.Messaging;
using Catalogo.AppService.Bibliotecas.Services;

namespace Catalogo.API.Consumers
{
    public class PaymentProcessedConsumer : BackgroundService
    {
        private readonly IMessageBus _messageBus;
        private readonly IBibliotecaAppServices _appServices;

        public PaymentProcessedConsumer(IMessageBus messageBus, IBibliotecaAppServices appServices)
        {
            _messageBus = messageBus;
            _appServices = appServices;
        }

        protected override  Task ExecuteAsync( CancellationToken stoppingToken)
        {
             _messageBus.SubscribeAsync<PaymentProcessedEvent>("payment-processed", IncluirJogoBiblioteca);

            return Task.Delay(Timeout.Infinite, stoppingToken);

        }

        private async Task IncluirJogoBiblioteca(PaymentProcessedEvent ev)
        {
            if (ev.Status == "Aprovado")
            {
                await _appServices.AdquirirJogo(ev);
            }
        }
    }
}
