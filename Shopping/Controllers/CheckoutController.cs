using Microsoft.AspNetCore.Mvc;
using Shopping.Data;

namespace Shopping.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ShoppingDbContext _db;

        public CheckoutController(ShoppingDbContext db)
        {
            _db = db;
        }

        public async  Task<IActionResult> Checkout()
        {
            return View();
        }
    }
}
