using System.Threading.Tasks;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DockerCoffee.Worker.Consumers
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        ILogger<OrderPlacedEvent> _logger;
        private readonly IOrderService _orderService;

        public OrderPlacedEventConsumer(ILogger<OrderPlacedEvent> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
        {
            _logger.LogInformation($"Preparing order #{context.Message.OrderId}");
            _orderService.Prepare(context.Message.OrderId);
        }
    }
}
