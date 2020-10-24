using System;
using System.Collections.Generic;
using DockerCoffee.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DockerCoffee.Shared
{
    public class CoffeeDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CoffeeDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Beverage> Beverages { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(_configuration.GetConnectionString("CoffeeDbContext"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beverage>().HasData(
                new Beverage { Id = 1, Name = "Espresso", Stock = 10, PreparationTime = 30 },
                new Beverage { Id = 2, Name = "Cappuccino", Stock = 2, PreparationTime = 60 },
                new Beverage { Id = 3, Name = "Americano", Stock = 5, PreparationTime = 45 }
            );
        }
    }
}
