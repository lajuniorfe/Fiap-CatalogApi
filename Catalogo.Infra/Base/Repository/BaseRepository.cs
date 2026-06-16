using Catalogo.Dominio.Base;
using Catalogo.Dominio.Base.Repository;
using Catalogo.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infra.Base.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {

            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Alterar(T entidade)
        {
            _context.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            _context.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            _context.Remove(ObterPorId(id));
            _context.SaveChanges();
        }

        public IList<T> ObterDados()
            => _dbSet.ToList();

        public T ObterPorId(Guid id)
            => _dbSet.FirstOrDefault(entity => entity.Id == id);
    }
}
