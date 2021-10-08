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
    public class RecurringJobScheduleService : IRecurringJobScheduleService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly CoffeeDbContext _coffeeDbContext;

        public RecurringJobScheduleService(ILogger<OrderService> logger, CoffeeDbContext coffeeDbContext)
        {
            _logger = logger;
            _coffeeDbContext = coffeeDbContext;
        }

        public List<RecurringJobSchedule> GetAll()
        {
            return _coffeeDbContext.RecurringJobSchedules
                .ToList();
        }
    }
}
