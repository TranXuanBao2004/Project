using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;

namespace Shopping.Controllers
{
    public class BrandController : Controller
    {
        private readonly ShoppingDbContext _db;

        public BrandController(ShoppingDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index(string Slug)
        {
            if (string.IsNullOrEmpty(Slug))
            {
                return RedirectToAction("Index");
            }

            var brand = await this._db.Brands.FirstOrDefaultAsync(x => x.Slug == Slug);
            if (brand == null)
            {
                return RedirectToAction("Index");
            }

            var productsByBrand = await this._db.Products
                                                .Where(x => x.BrandId == brand.Id)
                                                .ToListAsync();

            return View(productsByBrand);
        }
    }
}
