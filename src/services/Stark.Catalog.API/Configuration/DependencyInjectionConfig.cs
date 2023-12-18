using Stark.Catalog.API.Data.Repository;
using Stark.Catalog.API.Data;
using Stark.Catalog.API.Models;

namespace Stark.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}
