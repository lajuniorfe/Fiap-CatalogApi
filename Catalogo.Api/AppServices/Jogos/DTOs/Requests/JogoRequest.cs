using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Jogos.DTOs.Requests
{
    public class JogoRequest
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
