using System;
using System.Threading.Tasks;
using DockerCoffee.Shared.Configuration;
using DockerCoffee.Shared.Jobs;
using MassTransit;
using Microsoft.Extensions.Options;
using Quartz;

namespace DockerCoffee.Scheduler.Jobs
{
    public class MassTransitRecurringJobPublisher : IJob
    {
        private readonly IBus _bus;
        private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfig;
        public const string JobDataType = "Type";
        public const string JobDataMessage = "Message";

        public MassTransitRecurringJobPublisher(IBus bus, IOptions<RabbitMqConfiguration> rabbitMqConfig)
        {
            _bus = bus;
            _rabbitMqConfig = rabbitMqConfig;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var type = (RecurringJobType)dataMap.GetInt(JobDataType);

            var job = new RecurringJob()
            {
                Message = dataMap.GetString(JobDataMessage) ?? type.ToString(),
                Type = type,
            };
            
            await _bus.Publish(job, (ctx) =>
            {
                ctx.Headers.Set("x-deduplication-header", "DUP123");
            }, context.CancellationToken);
        }
    }
}
