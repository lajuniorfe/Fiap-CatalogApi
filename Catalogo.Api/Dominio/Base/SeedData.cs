using Catalogo.Dominio.Jogos;
using Catalogo.Infra.Context;

namespace Catalogo.Api.Dominio.Base
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Jogo.Any())
                return;

            context.Jogo.AddRange(
                new Jogo("God of War", "Aventura", 199.90m),

                new Jogo( "The Witcher 3", "RPG", 129.90m)
              );

            context.SaveChanges();
        }
    }
}
