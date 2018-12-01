using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Project.Controllers
{

    public class CartController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _http;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        
        }
        
        public IActionResult ExitPage()
        {
            return View();
        }
        public IActionResult Index()
        { 
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if(ViewBag.cart==null)
            {
                return View();
            }
            ViewBag.total = cart.Sum(item => item.Price * item.Quantity);
            return View();
        }
      public IActionResult OrderHistory()
        {
            List<Order> listoforders = new List<Order>();
            var user = HttpContext.User.Identity.Name;
            ShoppingCartViewModel cvm = new ShoppingCartViewModel();
            var orders = _context.Orders;
            listoforders.AddRange(orders);
            if (listoforders != null && listoforders.Count != 0)
            {
                
                foreach (Order order in listoforders)
                {
                    if (order.username.Equals(user.ToString()))
                    {
                        cvm.OrdersList.Add(order);
                    }
                }
                return View(cvm);
            }


            else
            {
                return View(cvm);
            }
        }
      [HttpPost]
        public IActionResult Checkout([FromForm] int total)
        {
            var user = HttpContext.User.Identity.Name;
            Order CurrentOrder = new Order();
            CurrentOrder.Total = total;
            CurrentOrder.username = user.ToString();   
            List<Order> allorders = new List<Order>();
            var orders = _context.Orders;
            int lastid = 0;
            /*if (orders != null)
            {
                allorders.AddRange(orders);
                if (allorders != null && allorders.Count != 0)
                {
                    lastid = allorders.Last().OrderId;
                    lastid++;
                    CurrentOrder.OrderId = lastid;
                }

                else
                    CurrentOrder.OrderId = lastid;
            }

            else
                CurrentOrder.OrderId = lastid;
*/
            _context.Orders.Add(CurrentOrder);
            _context.SaveChanges();

            return RedirectToAction("ExitPage");


        }
       
        [HttpPost]
        public IActionResult Buy([FromForm] int id, [FromForm] int quantity)
        {
            OrderItemViewModel model = new OrderItemViewModel();

            var items = _context.OrderItems;
            model.OrderItems.AddRange(items);

            var itemslist = items.ToList();

            if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItem> cart = new List<OrderItem>();
                foreach (OrderItem item1 in itemslist)
                    if (item1.OrderItemId == id)
                    {
                        item1.Quantity = quantity;
                        cart.Add(item1);

                    }
                    else
                        continue;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart").ToList();
                int index = ItemExists(id);
                if (index != -1)
                {
                    cart[index].Quantity = cart[index].Quantity + quantity;


                }
                else
                {
                    foreach (OrderItem aitem in itemslist)
                        if (aitem.OrderItemId == id)
                        {
                            aitem.Quantity = quantity;
                            cart.Add(aitem);

                        }
                        else
                            continue;

                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        private int ItemExists([FromForm] int id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            if (cart == null)
                return -2;
            else
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].OrderItemId == id)
                    {
                        return i;
                    }
                }


                return -1;
            }
           
        }

 
    
        [HttpPost]
        public IActionResult Remove([FromForm] int id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            int index = ItemExists(id);
            if (index == -2)
            {
                return RedirectToAction("MainPageView", "Home");
            }
            else
            {
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("Index");
            }
        }

    }
}
