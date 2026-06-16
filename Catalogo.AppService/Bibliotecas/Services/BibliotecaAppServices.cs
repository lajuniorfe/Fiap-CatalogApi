using Catalogo.AppService.BibliotecaJogos.DTOs.Responses;
using Catalogo.AppService.Bibliotecas.DTOs;
using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Bibliotecas.Repository;
using Catalogo.Infra.Logger;

namespace Catalogo.AppService.Bibliotecas.Services
{
    public  class BibliotecaAppServices: IBibliotecaAppServices
    {
        private readonly IBibliotecaRepository _bibliotecaRepository;
        private readonly BaseLogger<BibliotecaAppServices> _logger;

        public BibliotecaAppServices(IBibliotecaRepository bibliotecaRepository, BaseLogger<BibliotecaAppServices> logger)
        {
            _bibliotecaRepository = bibliotecaRepository;
            _logger = logger;

        }

        public void AdquirirJogo(AdquirirJogoRequest request)
        {
            try
            {
                Biblioteca biblioteca = _bibliotecaRepository.ObterPorUsuario(request.IdUsuario);

                if (biblioteca == null)
                {
                    biblioteca = CriarBibliotecaUsuario(request.IdUsuario);
                }

                biblioteca.AdicionarJogo(request.IdJogo);

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
                throw new Exception("Não há biblioteca de jogos para esse usuário");

            var response = new BibliotecaResponse()
            {
                IdUsuario = idUsuario,
                Jogos = bibliotecaRetornada.BibliotecaJogos.Select(bj => new BibliotecaJogoResponse
                {
                    Id = bj.Jogo.Id,
                    Nome = bj.Jogo.Nome,
                    Descricao = bj.Jogo.Descricao,
                    DataAquisicao = bj.DataAquisicao
                }).ToList()
            };

            return response;
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
