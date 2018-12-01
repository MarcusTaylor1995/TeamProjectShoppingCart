using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            this.OrdersList = new List<Order>();
        }
        public List<Order> OrdersList { get; set; }

        public decimal CartTotal { get; set; }
        
    }
}
