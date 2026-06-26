using Catalogo.Dominio.Jogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalogo.Infra.Jogos.Configuration
{
    internal class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("jogo");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).HasColumnType("TEXT").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("TEXT").IsRequired();
            builder.Property(p => p.Preco);

            // Relacionamento N:N via BibliotecaJogo
            builder.HasMany(j => j.BibliotecaJogos)
                   .WithOne(bj => bj.Jogo)
                   .HasForeignKey(bj => bj.JogoId);
        }
    }
}
