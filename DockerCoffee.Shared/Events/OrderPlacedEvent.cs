namespace DockerCoffee.Shared.Events
{
    public class OrderPlacedEvent
    {
        public OrderPlacedEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}