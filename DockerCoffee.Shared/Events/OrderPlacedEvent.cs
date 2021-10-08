using DockerCoffee.Shared.Entities;

namespace DockerCoffee.Shared.Events
{
    public class OrderPlacedEvent
    {
        public int OrderId { get; set; }
    }
}