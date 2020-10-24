using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Jobs;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DockerCoffee.Worker.Consumers
{
    public class RecurringJobConsumer : IConsumer<RecurringJob>
    {
        ILogger<RecurringJobConsumer> _logger;
        private readonly IBeverageService _beverageService;

        public RecurringJobConsumer(ILogger<RecurringJobConsumer> logger, IBeverageService beverageService)
        {
            _logger = logger;
            _beverageService = beverageService;
        }

        public async Task Consume(ConsumeContext<RecurringJob> context)
        {
            _logger.LogInformation($"Recurring job received: {context.Message.Message}");

            switch(context.Message.Type)
            {
                case RecurringJobType.Restock:
                    Restock();
                    break;
            }
        }

        private void Restock()
        {
            _beverageService.RestockAll();
        }
    }
}
