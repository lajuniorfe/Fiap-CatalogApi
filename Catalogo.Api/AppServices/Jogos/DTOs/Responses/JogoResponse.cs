using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Jogos.DTOs.Responses
{
    public class JogoResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
