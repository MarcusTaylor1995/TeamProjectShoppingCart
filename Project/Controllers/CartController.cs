using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
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
                int index = isExist(id);
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

        private int isExist([FromForm] int id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].OrderItemId == id)
                {
                    return i;
                }
            }
            return -1;
        }

     /*   [HttpPost]
        public IActionResult AddToCart([FromForm] int id, [FromForm] int Quantity)
        {

            var vm = new ShoppingCartViewModel();

            var cartitem = _context.OrderItems.SingleOrDefault(item => item.OrderItemId == id);
            vm.CartItems.Add(cartitem);


            return RedirectToAction("Index");
        }
        */
       /* [HttpPost]
        public IActionResult Index([FromForm] int id)
        {
            var vm = new ShoppingCartViewModel();
            var cartitem = _context.OrderItems.SingleOrDefault(item => item.OrderItemId.ToString().Equals(id));
            vm.CartItems.Add(cartitem);


            return View(vm);
        }
        */

        /*[HttpPost]
         * public IActionResult AddtoCart(int id)
        {
            var vm = new ShoppingCartViewModel();
            var cartitem = _context.OrderItems.Single(item => item.OrderItemId == id);
            vm.CartItems.Add(cartitem);
            return View(vm);
        }
        */
        /* public IActionResult Index()
         {
             var cart = ShoppingCart.GetCart(this.HttpContext);

             var sCVM = new ShoppingCartViewModel
             {
                 CartItems = cart.GetCartItems(),
                 CartTotal = cart.GetTotal()
             };

             return View(sCVM);
         }
         */
        /* public ActionResult AddToCart(int id)
         {

             // Retrieve the album from the database
             var addeditem = _context.OrderItems
                 .SingleOrDefault(item => item.OrderItemId == id);

             // Add it to the shopping cart
             var cart = ShoppingCart.GetCart(this.HttpContext);

             cart.AddToCart(addeditem);

             // Go back to the main store page for more shopping
             return RedirectToAction("Index");
         }
         */

        /*public ViewResult AddToCart(int id, string testName)
        {
            var vm = new ShoppingCartViewModel();
            var cartitem = _context.OrderItems.Single(item => item.OrderItemId == id);
            vm.CartItems.Add(cartitem);
            return View(vm);
        }
        */
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string albumName = _context.Carts
                 .Single(item => item.RecordId == id).OrderItem.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message =
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
    }
}
