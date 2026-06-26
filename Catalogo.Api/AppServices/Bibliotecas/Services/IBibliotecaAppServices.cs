using Catalogo.API.Events;
using Catalogo.AppService.Bibliotecas.DTOs;

namespace Catalogo.AppService.Bibliotecas.Services
{
    public interface IBibliotecaAppServices
    {
        Task AdquirirJogo(PaymentProcessedEvent ev);
        public BibliotecaResponse ListarBibliotecaUsuario(Guid idUsuario);
        Task PublicarOrdemPagamento(AdquirirJogoRequest request);
    }
}
