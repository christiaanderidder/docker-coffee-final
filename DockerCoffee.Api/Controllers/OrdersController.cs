using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCoffee.Shared.Contracts;
using DockerCoffee.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DockerCoffee.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetAll();
        }
    }
}
