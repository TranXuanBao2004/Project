using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoppingDbContext _db;

        public ProductController(ShoppingDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int Id)
        {
            var product =await this._db.Products.FirstOrDefaultAsync(x=>x.Id == Id);
            return View(product);
        }
    }
}
