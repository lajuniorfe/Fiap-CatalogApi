using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.BibliotecaJogos.DTOs.Responses
{
    public class BibliotecaJogoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAquisicao { get; set; }
    }
}
