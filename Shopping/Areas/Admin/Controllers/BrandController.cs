using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly ShoppingDbContext _db;
        public BrandController(ShoppingDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await this._db.Brands.OrderByDescending(x=>x.Id).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brandNew)
        {
            if (ModelState.IsValid)
            {
                brandNew.Slug = brandNew.Name.Replace(" ", "_");
                var slug = await this._db.Categories.FirstOrDefaultAsync(x => x.Slug == brandNew.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thương Hiệu Đã Tồn Tại");
                    return View(brandNew);
                }
                this._db.Brands.Add(brandNew);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Thêm Thương Hiệu Thành Công";
                return RedirectToAction("Index");

            }
            return View(brandNew);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result =await this._db.Brands.FirstOrDefaultAsync(x => x.Id == Id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brandEdit)
        {
            if (ModelState.IsValid)
            {
                brandEdit.Slug = brandEdit.Name.Replace(" ", "_");
                this._db.Brands.Update(brandEdit);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Edit Brand Success";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Brand editing failed";
            return View(brandEdit);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var brandDelete = await this._db.Brands.FirstOrDefaultAsync(x => x.Id == Id);
            if (brandDelete != null)
            {
                this._db.Brands.Remove(brandDelete);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Delete Brand Success";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Brand deletion failed";
            return View(brandDelete);
        }
    }
}
