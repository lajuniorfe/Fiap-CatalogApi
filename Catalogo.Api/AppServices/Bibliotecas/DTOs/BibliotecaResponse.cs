using Catalogo.AppService.BibliotecaJogos.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Bibliotecas.DTOs
{
    public class BibliotecaResponse
    {
        public Guid IdUsuario { get; set; }
        public IList<BibliotecaJogoResponse> Jogos { get; set; }
    }
}
