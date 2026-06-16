using Catalogo.AppService.Jogos.DTOs.Requests;
using Catalogo.AppService.Jogos.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Jogos.Services
{
    public class JogoAppService : IJogoAppService
    {
        public void AlterarJogo(Guid id, JogoRequest request)
        {
            throw new NotImplementedException();
        }

        public JogoResponse BuscarJogo(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CadastrarJogo(JogoRequest request)
        {
            throw new NotImplementedException();
        }

        public void ExcluirJogo(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<JogoResponse> ListarJogos()
        {
            throw new NotImplementedException();
        }
    }
}
