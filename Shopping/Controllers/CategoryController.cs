using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShoppingDbContext _db;

        public CategoryController(ShoppingDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string Slug)
        {
            var category = await this._db.Categories.Where(x=>x.Slug== Slug).FirstOrDefaultAsync();    
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var productByCategory = this._db.Products.Where(x => x.CategoryId == category.Id);

            return View(await productByCategory.OrderByDescending(x=>x.Id).ToListAsync());
        }
    }
}
