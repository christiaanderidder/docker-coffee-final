using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Scheduler.Jobs;
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
                    services.AddHostedService<TaskScheduler>();

                    services.AddQuartz(cfg =>
                    {
                        cfg.UseMicrosoftDependencyInjectionJobFactory();
                    });
                });
    }
}
