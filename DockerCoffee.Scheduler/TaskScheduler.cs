using DockerCoffee.Scheduler.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Entities;
using DockerCoffee.Shared.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace DockerCoffee.Scheduler
{
    public class TaskScheduler : IHostedService
    {
        private readonly ILogger<TaskScheduler> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IScheduler _scheduler;

        public TaskScheduler(ILogger<TaskScheduler> logger, ISchedulerFactory schedulerFactory, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await _scheduler.Start(cancellationToken);

            using var scope = _serviceScopeFactory.CreateScope();
            var recurringJobScheduleService = scope.ServiceProvider.GetRequiredService<IRecurringJobScheduleService>();

            foreach (var schedule in recurringJobScheduleService.GetAll())
            {
                await _scheduler.ScheduleJob(CreateJob(schedule), CreateTrigger(schedule), cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await _scheduler?.Shutdown(cancellationToken);

        private static IJobDetail CreateJob(RecurringJobSchedule schedule) => JobBuilder
            .Create<MassTransitRecurringJobPublisher>()
            .WithIdentity($"{schedule.Type}.job")
            .WithDescription($"{schedule.Type}")
            .UsingJobData(MassTransitRecurringJobPublisher.JobDataType, (int)schedule.Type)
            .Build();

        private static ITrigger CreateTrigger(RecurringJobSchedule schedule) => TriggerBuilder
            .Create()
            .WithIdentity($"{schedule.Type}.trigger")
            .WithCronSchedule(schedule.CronExpression)
            .WithDescription(schedule.CronExpression)
            .Build();
    }
}
