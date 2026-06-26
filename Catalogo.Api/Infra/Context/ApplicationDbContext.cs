using Catalogo.Dominio.Bibliotecas;
using Catalogo.Dominio.Jogos;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Jogo> Jogo { get; set; }
        public DbSet<Biblioteca> Biblioteca { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public void EnsureDatabaseCreated()
        {
            Database.Migrate();
        }
    }
}
