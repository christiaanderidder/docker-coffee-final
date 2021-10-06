using System.Threading.Tasks;
using DockerCoffee.Shared.Jobs;
using MassTransit;
using Quartz;

namespace DockerCoffee.Scheduler.Jobs
{
    public class MassTransitRecurringJobPublisher : IJob
    {
        private readonly IBus _bus;
        public const string JobDataType = "Type";
        public const string JobDataMessage = "Message";

        public MassTransitRecurringJobPublisher(IBus bus)
        {
            _bus = bus;
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
            
            await _bus.Publish(job, context.CancellationToken);
        }
    }
}
