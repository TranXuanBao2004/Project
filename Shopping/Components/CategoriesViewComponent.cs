using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;

namespace Shopping.Components
{
    public class CategoriesViewComponent:ViewComponent
    {
        private readonly ShoppingDbContext _db;

        public CategoriesViewComponent(ShoppingDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _db.Categories.ToListAsync());
        }
       
    }
}
