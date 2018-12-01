using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Project.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
      
        public string CartId { get; set; }
        public int Count { get; set; }
        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }
       
      
      
    }

    
}
