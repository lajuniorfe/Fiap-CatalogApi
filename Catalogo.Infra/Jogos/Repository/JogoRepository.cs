using Catalogo.Dominio.Jogos;
using Catalogo.Dominio.Jogos.Repository;
using Catalogo.Infra.Base.Repository;
using Catalogo.Infra.Context;

namespace Catalogo.Infra.Jogos.Repository
{
    public class JogoRepository : BaseRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
