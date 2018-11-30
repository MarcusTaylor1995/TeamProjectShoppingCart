using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public partial class ShoppingCart
    {
        public const string CartSessionKey = "CartId";
       
        string ShoppingCartId { get; set; }

       
        private static ApplicationDbContext _context;
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public void AddToCart(OrderItem product)
        {
            
            // Get the matching cart 
            var cartItem = _context.Carts.FirstOrDefault(
                c => c.CartId == ShoppingCartId && c.OrderItemId == product.OrderItemId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    OrderItemId = product.OrderItemId,
                    CartId = ShoppingCartId,
                    Count = 1,

                };
                _context.Carts.Add(cartItem);

            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {

            var cartItem = _context.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }
                // Save changes
                _context.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _context.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return _context.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
       
        public decimal GetTotal()
        {
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.OrderItem.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public static ShoppingCart GetCart(HttpContext context1)
        {
           
            var cart = new ShoppingCart(_context);
            cart.ShoppingCartId = cart.GetCartId(context1);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public string GetCartId(HttpContext context)
        {
            if (context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session.SetString(CartSessionKey,
                        context.User.Identity.Name);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            var session = context.Session.GetString(CartSessionKey);
            return session.ToString();
        }
      
    }
}
