using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<OrderItem> Items {get; set;}

    }
}
