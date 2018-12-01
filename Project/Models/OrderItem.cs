using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
      
        public Product Product { get; set; }
       
        public Cart Cart { get; set; }

        [Required]
        public int Quantity { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Category { get; set; }
      



      
    }
}
