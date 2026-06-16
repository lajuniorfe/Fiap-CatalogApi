using Catalogo.AppService.Jogos.DTOs.Requests;
using Catalogo.AppService.Jogos.DTOs.Responses;

namespace Catalogo.AppService.Jogos.Services
{
    public interface IJogoAppService
    {
        void CadastrarJogo(JogoRequest request);
        void AlterarJogo(Guid id, JogoRequest request);
        void ExcluirJogo(Guid id);
        IList<JogoResponse>ListarJogos();

        JogoResponse BuscarJogo(Guid id);   
    }
}
