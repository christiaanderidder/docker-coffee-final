using System.Collections.Generic;
using DockerCoffee.Shared.Entities;

namespace DockerCoffee.Shared.Contracts
{
    public interface IOrderService
    {
        public List<Order> GetAll();
        public bool PlaceOrder(int beverageId, string customer);
        void Prepare(int orderId);
    }
}