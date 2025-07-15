using Microsoft.Extensions.DependencyInjection;
using EcommerceDDD.Application.Interfaces;
using EcommerceDDD.Application.Services;

namespace EcommerceDDD.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar serviços de aplicação
            services.AddScoped<IPersonApplicationService, PersonApplicationService>();
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IOrderApplicationService, OrderApplicationService>();

            return services;
        }
    }
} 