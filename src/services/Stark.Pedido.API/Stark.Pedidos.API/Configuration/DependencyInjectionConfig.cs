using FluentValidation.Results;
using MediatR;
using Stark.Core.Mediator;
using Stark.Pedidos.API.Application.Commands;
using Stark.Pedidos.API.Application.Events;
using Stark.Pedidos.API.Application.Queries;
using Stark.Pedidos.Domain.Pedidos;
using Stark.Pedidos.Domain.Vouchers;
using Stark.Pedidos.Infra.Data;
using Stark.Pedidos.Infra.Data.Repository;
using Stark.WebAPI.Core.Usuario;

namespace Stark.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            
            // Commands
            services.AddScoped<IRequestHandler<AdicionarPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<PedidoRealizadoEvent>, PedidoEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            // Data
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<PedidosContext>();
        }
    }
}