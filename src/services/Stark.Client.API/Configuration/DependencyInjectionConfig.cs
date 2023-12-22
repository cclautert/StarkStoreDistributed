using FluentValidation.Results;
using MediatR;
using Stark.Client.API.Applications.Commands;
using Stark.Client.API.Applications.Events;
using Stark.Client.API.Data;
using Stark.Client.API.Data.Repository;
using Stark.Client.API.Models;
using Stark.Core.Mediator;

namespace Stark.Client.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
        }
    }
}
