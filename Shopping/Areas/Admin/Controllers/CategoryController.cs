using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ShoppingDbContext _db;

        public CategoryController(ShoppingDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this._db.Categories.OrderByDescending(x=>x.Id).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category categoryNew)
        {
            if (ModelState.IsValid)
            {
                categoryNew.Slug = categoryNew.Name.Replace(" ", "_");
                var slug = await this._db.Categories.FirstOrDefaultAsync(x => x.Slug == categoryNew.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh Mục Đã Tồn Tại");
                    return View(categoryNew);
                }
                this._db.Categories.Add(categoryNew);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Thêm Danh Mục Thành Công";
                return RedirectToAction("Index");
                
            }
            return View(categoryNew);
        }
        [HttpGet]
        public async Task<IActionResult>Edit(int Id)
        {
            var productEdit = await this._db.Categories.FirstOrDefaultAsync(x=>x.Id == Id);
            return View(productEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(Category categoryEdit)
        {
            if (ModelState.IsValid)
            {
                categoryEdit.Slug = categoryEdit.Name.Replace(" ", "_");
                this._db.Categories.Update(categoryEdit);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Edit Category Success";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Category editing failed";
            return View(categoryEdit);
        }
        public async Task<IActionResult>Delete(int Id)
        {
            var productDelete = await this._db.Categories.FirstOrDefaultAsync(x => x.Id == Id);
            if (productDelete!=null)
            {
                this._db.Categories.Remove(productDelete);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Delete Category Success";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Category deletion failed";
            return View(productDelete);
        }

    }
}
