using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DockerCoffee.Shared.Configuration;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Services;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCoffee.Shared
{
    public static class SharedServiceCollectionExtensions
    {
        public static void AddDockerCoffeeShared(this IServiceCollection services)
        {
            services.AddDbContext<CoffeeDbContext>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBeverageService, BeverageService>();
        }

        public static void AddAndConfigureMassTransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollectionBusConfigurator> configure = null)
        {
            var rabbitMqConfig = config.GetSection(RabbitMqConfiguration.Section).Get<RabbitMqConfiguration>();

            if (rabbitMqConfig == null) return;

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqConfig.Host);
                    cfg.ConfigureEndpoints(context);
                });

                // Allows the caller to set publishers and consumers
                if (configure != null) configure(cfg);
            });
        }
    }
}
