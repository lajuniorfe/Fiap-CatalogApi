using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Bibliotecas.DTOs
{
    public class AdquirirJogoRequest
    {
        public Guid IdUsuario { get; set; }
        public Guid IdJogo { get; set; }
    }
}
