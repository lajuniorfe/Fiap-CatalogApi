using Catalogo.AppService.Jogos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Users.AppService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAplicationCollection(this IServiceCollection services)
        {
            services.AddTransient<IJogoAppService, JogoAppService>();

            return services;
        }
    }
}
