using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Scheduler.Jobs;
using DockerCoffee.Shared;
using DockerCoffee.Shared.Jobs;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace DockerCoffee.Scheduler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MassTransitRecurringJobPublisher>();
                    
                    services.AddQuartz(cfg =>
                    {
                        cfg.UseMicrosoftDependencyInjectionJobFactory();
                    });
                    
                    services.AddMassTransitHostedService(true);
                    services.AddAndConfigureMassTransit(hostContext.Configuration);
                    services.AddHostedService<TaskScheduler>();
                });
    }
}
