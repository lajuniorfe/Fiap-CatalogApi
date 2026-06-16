using Catalogo.Dominio.Base;
using Catalogo.Dominio.BibliotecaJogos;

namespace Catalogo.Dominio.Jogos
{
    public class Jogo: EntityBase
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        public ICollection<BibliotecaJogo> BibliotecaJogos { get; set; }


        protected Jogo() { }

        public Jogo(string nome, string descricao, decimal preco)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }
    }
}
