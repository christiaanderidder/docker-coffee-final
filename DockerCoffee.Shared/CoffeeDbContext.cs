using System;
using System.Collections.Generic;
using DockerCoffee.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DockerCoffee.Shared
{
    public class CoffeeDbContext : DbContext
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {
        }

        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RecurringJobSchedule> RecurringJobSchedules { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beverage>().HasData(
                new Beverage { Id = 1, Name = "Espresso", Stock = 10, PreparationTime = 30 },
                new Beverage { Id = 2, Name = "Cappuccino", Stock = 2, PreparationTime = 60 },
                new Beverage { Id = 3, Name = "Americano", Stock = 5, PreparationTime = 45 }
            );

            modelBuilder.Entity<RecurringJobSchedule>().HasData(
                new RecurringJobSchedule { Id = 1, Type = RecurringJobType.Restock, CronExpression = "*/10 * * * * ?" }
            );
        }
    }
}
