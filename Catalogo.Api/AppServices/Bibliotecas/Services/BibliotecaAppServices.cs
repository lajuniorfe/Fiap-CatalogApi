using Catalogo.API.Events;
using Catalogo.API.Messaging;
using Catalogo.AppService.BibliotecaJogos.DTOs.Responses;
using Catalogo.AppService.Bibliotecas.DTOs;
using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Bibliotecas.Repository;
using Catalogo.Dominio.Jogos;
using Catalogo.Dominio.Jogos.Repository;
using Catalogo.Infra.Logger;

namespace Catalogo.AppService.Bibliotecas.Services
{
    public class BibliotecaAppServices : IBibliotecaAppServices
    {
        private readonly IBibliotecaRepository _bibliotecaRepository;
        private readonly BaseLogger<BibliotecaAppServices> _logger;
        private readonly IMessageBus _publisher;
        private readonly IJogoRepository jogoRepository;


        public BibliotecaAppServices(IBibliotecaRepository bibliotecaRepository, BaseLogger<BibliotecaAppServices> logger, IMessageBus publisher, IJogoRepository jogoRepository)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _logger = logger;
            _publisher = publisher;
            this.jogoRepository = jogoRepository;
        }

        public async Task PublicarOrdemPagamento(AdquirirJogoRequest request)
        {
            Jogo jogoRetornado = jogoRepository.ObterPorId(request.IdJogo);

            var ordemPagamento = new OrderPlacedEvent()
            {
                GameId = request.IdJogo,
                Price = jogoRetornado.Preco,
                UserId = request.IdUsuario,
            };

            await _publisher.PublishAsync("order-placed", ordemPagamento);
        }

        public async Task AdquirirJogo(PaymentProcessedEvent ev)
        {
            try
            {
                Biblioteca biblioteca = _bibliotecaRepository.ObterPorUsuario(ev.UserId);

                if (biblioteca == null)
                {
                    biblioteca = CriarBibliotecaUsuario(ev.UserId);
                }

                biblioteca.AdicionarJogo(ev.GameId);

                _bibliotecaRepository.Alterar(biblioteca);

                _logger.LogInformation($"Jogo adquirido com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adquirir Jogo: {ex.Message}");
                throw new Exception("Erro ao inserir jogo na biblioteca do usuario", ex);
            }
        }

        public BibliotecaResponse ListarBibliotecaUsuario(Guid idUsuario)
        {
            Biblioteca bibliotecaRetornada = _bibliotecaRepository.ObterPorUsuario(idUsuario);

            if (bibliotecaRetornada == null)
                return null;

            var jogos = bibliotecaRetornada.BibliotecaJogos
                .Select(bj => jogoRepository.ObterPorId(bj.JogoId))
                .Where(jogo => jogo != null)
                .Select(jogo => new BibliotecaJogoResponse
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Descricao = jogo.Descricao,
                    DataAquisicao = bibliotecaRetornada.BibliotecaJogos
                        .First(bj => bj.JogoId == jogo.Id)
                        .DataAquisicao
                })
                .ToList();

            return new BibliotecaResponse
            {
                IdUsuario = idUsuario,
                Jogos = jogos
            };
        }

        private Biblioteca CriarBibliotecaUsuario(Guid idUsuario)
        {
            try
            {
                Biblioteca biblioteca = new(idUsuario);
                _bibliotecaRepository.Cadastrar(biblioteca);

                _logger.LogInformation($"Biblioteca criada com sucesso!");

                return biblioteca;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar biblioteca do usuário", ex);
            }
        }

    }
}
