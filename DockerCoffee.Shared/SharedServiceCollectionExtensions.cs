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

        public static void AddAndConfigureMassTransit(this IServiceCollection services, IConfiguration config, Action<IServiceCollectionBusConfigurator> configure = null)
        {
            var rabbitMqConfig = config.GetSection(RabbitMqConfiguration.Section).Get<RabbitMqConfiguration>();

            services.AddTransient<IConfigureReceiveEndpoint, ConfigureDeduplicationReceiveEndpoint>();
            services.Configure<RabbitMqConfiguration>(config.GetSection(RabbitMqConfiguration.Section));
            
            if (rabbitMqConfig == null) return;

            services.AddMassTransit(cfg =>
            {
                
                cfg.UsingRabbitMq((ctx, busCfg) =>
                {
                    busCfg.Host(rabbitMqConfig.Host);
                    
                    // Allows the caller to set consumers
                    configure?.Invoke(cfg);
                    
                    busCfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
    
    public class ConfigureDeduplicationReceiveEndpoint : IConfigureReceiveEndpoint
    {
        /// <summary>
        /// Configures the receive endpoint. The given endpoint will be defined as a quorum in RabbitMQ.
        /// </summary>
        public void Configure(string name, IReceiveEndpointConfigurator configurator)
        {
            if (configurator is IRabbitMqReceiveEndpointConfigurator rabbitMqConfigurator)
            {
                rabbitMqConfigurator.SetQueueArgument("x-message-deduplication", true);
            }
        }
    }
}
