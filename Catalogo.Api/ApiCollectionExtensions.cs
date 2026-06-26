
using Catalogo.API.Messaging;
using Catalogo.AppService.Bibliotecas.Services;
using Catalogo.AppService.Jogos.Services;
using Catalogo.Dominio.Bibliotecas.Repository;
using Catalogo.Dominio.Jogos.Repository;
using Catalogo.Infra.Bibliotecas.Repository;
using Catalogo.Infra.Jogos.Repository;
using Catalogo.Infra.Logger;

namespace Catalogo.Api
{
    public static class ApiCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
        {
            services.AddTransient<IJogoAppService, JogoAppService>();
            services.AddTransient<IBibliotecaAppServices, BibliotecaAppServices>();
            services.AddTransient<IJogoRepository, JogoRepository>();
            services.AddTransient<IBibliotecaRepository, BibliotecaRepository>();
            services.AddTransient<IMessageBus, RabbitMqMessageBus>();


            services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();

            return services;
        }
    }
}
