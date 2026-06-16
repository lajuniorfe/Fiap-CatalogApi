using Catalogo.Dominio.Base;
using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Jogos;

namespace Catalogo.Dominio.BibliotecaJogos
{
    public class BibliotecaJogo: EntityBase
    {
        public DateTime DataAquisicao { get; set; }

        public Guid BibliotecaId { get; set; }

        public Guid JogoId { get; set; }

        public Biblioteca Biblioteca { get; set; } = null!;

        public Jogo Jogo { get; set; } = null!;
    }
}
