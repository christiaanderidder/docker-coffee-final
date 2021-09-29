using DockerCoffee.Scheduler.Jobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DockerCoffee.Scheduler
{
    public class TaskScheduler : IHostedService
    {
        private static readonly List<RecurringJobSchedule> _dummySchedules = new List<RecurringJobSchedule>()
        {
            new RecurringJobSchedule()
            {
                Type = RecurringJobType.Restock,
                CronExpression = "*/10 * * * * ?"
            },
        };

        private readonly ILogger<TaskScheduler> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public TaskScheduler(ILogger<TaskScheduler> logger, ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await _scheduler.Start(cancellationToken);

            foreach (var schedule in _dummySchedules)
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
