using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Bibliotecas.Repository;
using Catalogo.Infra.Base.Repository;
using Catalogo.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infra.Bibliotecas.Repository
{
    public class BibliotecaRepository : BaseRepository<Biblioteca>, IBibliotecaRepository
    {
        public BibliotecaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Biblioteca ObterPorUsuario(Guid idUsuario)
        {
            return _context.Biblioteca
                 .Include(b => b.BibliotecaJogos)
                 .ThenInclude(j => j.Jogo)
                 .FirstOrDefault(b => b.UsuarioId == idUsuario);
        }
    }
}
