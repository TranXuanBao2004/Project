using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shopping.Data;
using Shopping.Helpers;
using Shopping.Models;
using Shopping.Models.ViewModels;

namespace Shopping.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingDbContext _db;

        public CartController(ShoppingDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItemViewModel cartItemVm = new()
            {
                CartItem = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartItemVm);
        }
        public async Task<IActionResult> AddToCart(int Id)
        {
            var productCart = await this._db.Products.FindAsync(Id);
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItems = cart.FirstOrDefault(c=>c.ProductId== Id);
            if (cartItems == null)
            { 
                cart.Add(new CartItem(productCart));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.Set("Cart", cart);
            TempData["success"] = "Add Item To Cart Successfully";
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> RemoveToCart(int Id)
        {
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.RemoveAll(p => p.ProductId == Id);
            if (cart.Count==0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Set("Cart",cart);
            }
            TempData["success"] = "Remove Item Of Cart Successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["success"] = "Clear Item Of Cart Successfully";
            return RedirectToAction("Index");
        }

    }
}
