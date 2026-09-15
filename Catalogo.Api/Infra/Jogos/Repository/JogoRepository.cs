using Catalogo.Dominio.Jogos;
using Catalogo.Dominio.Jogos.Repository;
using Catalogo.Infra.Base.Repository;
using MongoDB.Driver;

namespace Catalogo.Infra.Jogos.Repository
{
    public class JogoRepository : BaseRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
