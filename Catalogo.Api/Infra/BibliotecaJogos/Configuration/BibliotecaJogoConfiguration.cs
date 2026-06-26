using Catalogo.Dominio.BibliotecaJogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalogo.Infra.BibliotecaJogos.Configuration
{
    internal class BibliotecaJogoConfiguration : IEntityTypeConfiguration<BibliotecaJogo>
    {
        public void Configure(EntityTypeBuilder<BibliotecaJogo> builder)
        {
            builder.ToTable("bibliotecaJogo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataAquisicao)
                   .IsRequired();

            builder.HasOne(x => x.Biblioteca)
                   .WithMany(b => b.BibliotecaJogos)
                   .HasForeignKey(x => x.JogoId)
                   .HasForeignKey(bj => bj.BibliotecaId);

            builder.HasOne(bj => bj.Jogo)
               .WithMany(j => j.BibliotecaJogos)
               .HasForeignKey(bj => bj.JogoId);
        }
    }
}
