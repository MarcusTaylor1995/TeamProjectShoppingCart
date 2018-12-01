using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Order
    {
            [Key]
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public string username { get; set; }


    }
}
