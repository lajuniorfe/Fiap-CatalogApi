
using Catalogo.Infra.Logger;

namespace Catalogo.Api
{
    public static class ApiCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdGenerator(this IServiceCollection services)
        {
            return services.AddTransient<ICorrelationIdGenerator, CorrelationIdGenerator>();
        }
    }
}
