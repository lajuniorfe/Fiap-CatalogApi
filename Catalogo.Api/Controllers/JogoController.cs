using Catalogo.AppService.Jogos.DTOs.Requests;
using Catalogo.AppService.Jogos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class JogoController : ControllerBase
    {
        private readonly IJogoAppService _jogoAppService;

        public JogoController(IJogoAppService jogoAppService)
        {
            _jogoAppService = jogoAppService;
        }

        /// <summary>
        /// Cadastra Jogo.
        /// </summary>
        /// <remarks>
        /// O usuário deve estar autenticado com a policy "Admin".
        /// </remarks>
        /// <param name="request">Dados contendo informações do Jogo.</param>
        /// <returns>Retorna sucesso caso o jogo seja cadastrado.</returns>
        /// <response code="200">Jogo cadastrado com sucesso.</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="401">Usuário não autorizado</response>
        [HttpPost]
        //[Authorize(Policy = "Admin")]
        public IActionResult CadastrarJogo([FromBody] JogoRequest request)
        {
            _jogoAppService.CadastrarJogo(request);
            return Created();
        }

        /// <summary>
        /// Retorna Jogo.
        /// </summary>
        /// <param name="request">Dados contendo informações do Jogo.</param>
        /// <returns>Retorna sucesso caso o jogo exista.</returns>
        /// <response code="200">Jogo retornado com sucesso.</response>
        /// <response code="400">Requisição inválida</response>
        [HttpGet("id")]
        public IActionResult RetornarJogo(Guid id)
        {
            return Ok(_jogoAppService.BuscarJogo(id));
        }

        /// <summary>
        /// Retorna lista de Jogos.
        /// </summary>
        /// <returns>Retorna sucesso caso a lista de jogos exista.</returns>
        /// <response code="200">Lista de jogos retornada com sucesso.</response>
        /// <response code="400">Requisição inválida</response>
        [HttpGet]
        public IActionResult RetornarJogos()
        {
            return Ok(_jogoAppService.ListarJogos());
        }

    }
}
