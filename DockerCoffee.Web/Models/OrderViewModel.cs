using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCoffee.Web.Models
{
    public class OrderViewModel
    {
        public int BeverageId { get; set; }
        public string Customer { get; set; }
    }
}
