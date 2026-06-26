using Catalogo.AppService.Jogos.DTOs.Requests;
using Catalogo.AppService.Jogos.DTOs.Responses;
using Catalogo.Dominio.Jogos;
using Catalogo.Dominio.Jogos.Repository;
using Catalogo.Infra.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.AppService.Jogos.Services
{
    public class JogoAppService : IJogoAppService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly BaseLogger<JogoAppService> _logger;


        public JogoAppService(IJogoRepository jogoRepository, BaseLogger<JogoAppService> logger)
        {
            _jogoRepository = jogoRepository;
            _logger = logger;
        }

        public JogoResponse BuscarJogo(Guid id)
        {
            try
            {
                Jogo? retorno = _jogoRepository.ObterPorId(id);

                if (retorno != null)
                {
                    JogoResponse response = new()
                    {
                        Id = retorno.Id,
                        Nome = retorno.Nome,
                        Descricao = retorno.Descricao,
                        Preco = retorno.Preco
                    };

                    return response;
                }

                throw new Exception("Jogo inexistente");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar jogo: {ex.Message}");
                throw new Exception("Erro ao buscar jogo!");
            }
        }

        public void CadastrarJogo(JogoRequest request)
        {
            try
            {
                Jogo jogo = new(request.Nome, request.Descricao, request.Preco);
                _jogoRepository.Cadastrar(jogo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao cadastrar jogo: {ex.Message}");
                throw new Exception("Erro ao cadastrar jogo!");
            }
        }

    

        public IList<JogoResponse> ListarJogos()
        {
            try
            {
                IList<Jogo> retorno = _jogoRepository.ObterDados();

                IList<JogoResponse> response = new List<JogoResponse>();

                foreach (var i in retorno)
                {
                    JogoResponse jogoResponse = new JogoResponse()
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        Descricao = i.Descricao,
                        Preco = i.Preco
                    };

                    response.Add(jogoResponse);
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao buscar jogos: {ex.Message}");
                throw new Exception("Erro ao buscar jogos!");
            }
        }
    }
}
