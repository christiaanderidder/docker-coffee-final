using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerCoffee.Shared.Entities
{
    public class Order : BaseEntity
    {
        public string Customer { get; set; }
        public OrderStatus Status { get; set; }

        public Beverage Beverage { get; set; }
        public int BeverageId { get; set; }
    }
}
