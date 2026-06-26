using Catalogo.Dominio.Bibliotecas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infra.Bibliotecas.Configuration
{
    internal class BibliotecaConfiguration : IEntityTypeConfiguration<Biblioteca>
    {
        public void Configure(EntityTypeBuilder<Biblioteca> builder)
        {
            builder.ToTable("biblioteca");

            builder.HasKey(b => b.Id);

            // Relacionamento N:N via BibliotecaJogo
            builder.HasMany(b => b.BibliotecaJogos)
                   .WithOne(bj => bj.Biblioteca)
                   .HasForeignKey(bj => bj.BibliotecaId);
        }
    }
}
