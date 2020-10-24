using System.Collections.Generic;
using DockerCoffee.Shared.Entities;

namespace DockerCoffee.Shared.Contracts
{
    public interface IBeverageService
    {
        public List<Beverage> GetAll();
        void RestockAll();
    }
}