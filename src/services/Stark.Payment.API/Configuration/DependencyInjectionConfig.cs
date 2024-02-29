using Stark.Payment.API.Data;
using Stark.Payment.API.Data.Repository;
using Stark.Payment.API.Facade;
using Stark.Payment.API.Models;
using Stark.Payment.API.Services;
using Stark.WebAPI.Core.Usuario;

namespace Stark.Payment.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IPagamentoFacade, PagamentoCartaoCreditoFacade>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<PagamentosContext>();
        }
    }
}
