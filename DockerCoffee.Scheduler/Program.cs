using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Scheduler.Jobs;
using DockerCoffee.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace DockerCoffee.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDockerCoffeeShared();
                    services.AddSingleton<MassTransitRecurringJobPublisher>();
                    services.AddHostedService<TaskScheduler>();

                    services.AddQuartz(cfg =>
                    {
                        cfg.UseMicrosoftDependencyInjectionJobFactory();
                    });

                    services.AddAndConfigureMassTransit(hostContext.Configuration);
                });
    }
}
