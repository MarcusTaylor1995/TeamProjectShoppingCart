using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MainPageView()
        {
           
           

            OrderItemViewModel model = new OrderItemViewModel();
            
            var items = _context.OrderItems;
            model.OrderItems.AddRange(items);

            return View(model);
        }

      
        public IActionResult CartView()
        {
            return View();
        }
       
        public IActionResult Sports()
        {
            OrderItemViewModel model = new OrderItemViewModel();

            var items = _context.OrderItems;
            model.OrderItems.AddRange(items);

            return View(model);
        }
        public IActionResult School()
        {
            OrderItemViewModel model = new OrderItemViewModel();

            var items = _context.OrderItems;
            model.OrderItems.AddRange(items);

            return View(model);
        }
        public IActionResult Technology()
        {
            OrderItemViewModel model = new OrderItemViewModel();

            var items = _context.OrderItems;
            model.OrderItems.AddRange(items);

            return View(model);
        }

        /*    public ViewResult List(string category)
        {
            string _category = category;
            List<OrderItem> orderitems = new List<OrderItem>();
            List<OrderItem> orderitems1 = new List<OrderItem>();
            string currentcategory = string.Empty;
           
            if (string.IsNullOrEmpty(category))
            {
                orderitems = _context.OrderItems.OrderBy(n => n.OrderItemId).ToList();
            }
            else
            {
                if (string.Equals("Soccer", _category, StringComparison.OrdinalIgnoreCase))
                {
                    orderitems = _context.OrderItems.Where(p => p.Category.Equals("Soccer")).OrderBy(p => p.Name).ToList();
                }

                else
                {
                    orderitems = null;
                }

                currentcategory = _category;
             }
           var viewmodel = new OrderItemViewModel
            {
                OrderItems = orderitems,
                Category = currentcategory
            };
            return View(viewmodel);
        }
        */
    }
}
