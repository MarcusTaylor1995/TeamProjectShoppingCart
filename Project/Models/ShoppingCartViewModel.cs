using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ShoppingCartViewModel
    {
        public List<OrderItem> CartItems { get; set; }

        public decimal CartTotal { get; set; }
        
    }
}
