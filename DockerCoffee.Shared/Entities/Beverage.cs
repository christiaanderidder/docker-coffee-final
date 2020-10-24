using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerCoffee.Shared.Entities
{
    public class Beverage : BaseEntity
    {
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public int Stock { get; set; }
    }
}
