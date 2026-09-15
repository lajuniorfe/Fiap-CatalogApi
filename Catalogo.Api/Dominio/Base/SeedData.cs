using Catalogo.Dominio.Jogos;
using MongoDB.Driver;

namespace Catalogo.Api.Dominio.Base
{
    public static class SeedData
    {
        public static void Seed(IMongoDatabase database)
        {
            var collection = database.GetCollection<Jogo>("Jogo");

            if (collection.Find(_ => true).Any())
                return;

            var jogos = new List<Jogo>
            {
                new Jogo("God of War", "Aventura", 199.90m),
                new Jogo("The Witcher 3", "RPG", 129.90m),
                new Jogo("The Last Of Us", "Ação", 184.90m)
            };

            collection.InsertMany(jogos);
        }
    }
}