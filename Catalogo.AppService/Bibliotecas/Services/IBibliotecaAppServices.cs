using Catalogo.AppService.Bibliotecas.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Bibliotecas.Services
{
    public interface IBibliotecaAppServices
    {
        void AdquirirJogo(AdquirirJogoRequest request);
        public BibliotecaResponse ListarBibliotecaUsuario(Guid idUsuario);
    }
}
