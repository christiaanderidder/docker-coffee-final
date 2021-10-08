using System;
using DockerCoffee.Shared.Configuration;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Services;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
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

        public static void AddAndConfigureMassTransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollectionBusConfigurator, IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> configure = null)
        {
            var rabbitMqConfig = config.GetSection(RabbitMqConfiguration.Section).Get<RabbitMqConfiguration>();

            services.Configure<RabbitMqConfiguration>(config.GetSection(RabbitMqConfiguration.Section));
            
            if (rabbitMqConfig == null) return;

            services.AddMassTransit(cfg =>
            {
                
                cfg.UsingRabbitMq((ctx, busCfg) =>
                {
                    //busCfg.ExchangeType = "x-message-deduplication";
                    busCfg.Host(rabbitMqConfig.Host);
                    //busCfg.ConfigureEndpoints(ctx);
                    
                    // Allows the caller to set publishers and consumers
                    configure?.Invoke(cfg, ctx, busCfg);
                });
            });
        }
    }
}
