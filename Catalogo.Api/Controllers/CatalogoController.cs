using Catalogo.AppService.Bibliotecas.DTOs;
using Catalogo.AppService.Bibliotecas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CatalogoController : ControllerBase
    {
        private readonly IBibliotecaAppServices bibliotecaAppServices;
        public CatalogoController(IBibliotecaAppServices bibliotecaAppServices)
        {
            this.bibliotecaAppServices = bibliotecaAppServices;
        }

        [HttpPost]
        public IActionResult InserirJogoBibliotecaUsuario([FromBody] AdquirirJogoRequest request)
        {
            bibliotecaAppServices.PublicarOrdemPagamento(request);

            return Ok();
        }
    }
}
