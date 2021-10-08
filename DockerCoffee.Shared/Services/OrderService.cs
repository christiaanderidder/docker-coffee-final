using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Entities;
using DockerCoffee.Shared.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DockerCoffee.Shared.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly CoffeeDbContext _coffeeDbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(ILogger<OrderService> logger, CoffeeDbContext coffeeDbContext, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _coffeeDbContext = coffeeDbContext;
            _publishEndpoint = publishEndpoint;
        }

        public List<Order> GetAll()
        {
            return _coffeeDbContext.Orders
                .Include(o => o.Beverage)
                .ToList();
        }

        public bool PlaceOrder(int beverageId, string customer)
        {
            try
            { 
                var beverage = _coffeeDbContext.Beverages.Find(beverageId);
                if (beverage == null || beverage.Stock <= 0) return false;

                var order = new Order()
                {
                    Beverage = beverage,
                    Customer = customer
                };

                _coffeeDbContext.Orders.Add(order);

                _coffeeDbContext.SaveChanges();

                _publishEndpoint.Publish(new OrderPlacedEvent() { OrderId = order.Id });

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to place order.");
                return false;
            }
        }

        public void Prepare(int orderId)
        {
            var order = _coffeeDbContext.Orders
                .Include(o => o.Beverage)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null) return;

            if (order.Beverage.Stock <= 0)
            {
                order.Status = OrderStatus.Failed;
                _coffeeDbContext.SaveChanges();
                return;
            }

            order.Status = OrderStatus.Preparing;
            _coffeeDbContext.SaveChanges();

            Thread.Sleep(TimeSpan.FromSeconds(order.Beverage.PreparationTime));

            order.Status = OrderStatus.Done;
            _coffeeDbContext.SaveChanges();
        }
    }
}
