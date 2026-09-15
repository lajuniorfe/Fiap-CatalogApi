using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Bibliotecas.Repository;
using Catalogo.Infra.Base.Repository;
using MongoDB.Driver;

namespace Catalogo.Infra.Bibliotecas.Repository
{
    public class BibliotecaRepository
        : BaseRepository<Biblioteca>, IBibliotecaRepository
    {
        public BibliotecaRepository(IMongoDatabase database)
            : base(database)
        {
        }

        public Biblioteca ObterPorUsuario(Guid idUsuario)
        {
            return _collection
                .Find(b => b.UsuarioId == idUsuario)
                .FirstOrDefault();
        }
    }
}
