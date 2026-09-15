using Catalogo.Dominio.Base;
using Catalogo.Dominio.Base.Repository;
using MongoDB.Driver;

namespace Catalogo.Infra.Base.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>  where T : EntityBase
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(
                typeof(T).Name);
        }

        public void Alterar(T entidade)
        {
            _collection.ReplaceOne(
                x => x.Id == entidade.Id,
                entidade);
        }

        public void Cadastrar(T entidade)
        {
            _collection.InsertOne(entidade);
        }

        public void Deletar(Guid id)
        {
            _collection.DeleteOne(
                x => x.Id == id);
        }

        public IList<T> ObterDados()
        {
            return _collection
                .Find(_ => true)
                .ToList();
        }

        public T ObterPorId(Guid id)
        {
            return _collection
                .Find(x => x.Id == id)
                .FirstOrDefault();
        }
    }
}