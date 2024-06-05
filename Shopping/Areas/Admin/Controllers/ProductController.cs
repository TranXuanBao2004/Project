using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Models;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ShoppingDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ShoppingDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this._db.Products.OrderByDescending(p => p.Id).Include(x => x.Category).Include(x => x.Brand).ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(this._db.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(this._db.Brands, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            ViewBag.Categories = new SelectList(this._db.Categories, "Id", "Name", model.CategoryId);
            ViewBag.Brands = new SelectList(this._db.Brands, "Id", "Name", model.BrandId);
            if (ModelState.IsValid)
            {
                TempData["success"] = "Thêm Thành Công";

                model.Slug = model.Name.Replace(" ", "_");
                var slug = await this._db.Products.FirstOrDefaultAsync(x => x.Slug == model.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Sản Phẩm Đã Tồn Tại");
                    return View(model);
                }

                if (model.UploadImage != null)
                {
                    string uploadsDir = Path.Combine(this._webHostEnvironment.WebRootPath, "images/Product");
                    string ImageName = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                    string filePath = Path.Combine(uploadsDir, ImageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await model.UploadImage.CopyToAsync(fs);
                    fs.Close();
                    model.Images = ImageName;
                }
                this._db.Products.Add(model);
                await this._db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error !!!!!!!!";
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var productEdit = await this._db.Products.FirstOrDefaultAsync(x => x.Id == Id);
            ViewBag.Categories = new SelectList(this._db.Categories, "Id", "Name", productEdit.CategoryId);
            ViewBag.Brands = new SelectList(this._db.Brands, "Id", "Name", productEdit.BrandId);
            return View(productEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product productEdit)
        {

            ViewBag.Categories = new SelectList(this._db.Categories, "Id", "Name", productEdit.CategoryId);
            ViewBag.Brands = new SelectList(this._db.Brands, "Id", "Name", productEdit.BrandId);
            if (ModelState.IsValid)
            {


                productEdit.Slug = productEdit.Name.Replace(" ", "_");
                var slug = await this._db.Products.FirstOrDefaultAsync(x => x.Slug == productEdit.Slug);
                if (productEdit.UploadImage != null)
                {
                    string uploadsDir = Path.Combine(this._webHostEnvironment.WebRootPath, "images/Product");
                    string ImageName = Guid.NewGuid().ToString() + "_" + productEdit.UploadImage.FileName;
                    string filePath = Path.Combine(uploadsDir, ImageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await productEdit.UploadImage.CopyToAsync(fs);
                    fs.Close();
                    productEdit.Images = ImageName;
                }
                this._db.Products.Update(productEdit);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Edit Success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Error !!!!!!!!";
            }
            return View(productEdit);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var productDelete = await this._db.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (productDelete != null)
            {
                this._db.Products.Remove(productDelete);
                await this._db.SaveChangesAsync();
                TempData["success"] = "Delete Success";
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}




