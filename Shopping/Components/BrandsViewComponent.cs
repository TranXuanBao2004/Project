using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;

namespace Shopping.Components
{
	public class BrandsViewComponent:ViewComponent
	{
		private readonly ShoppingDbContext _db;

		public BrandsViewComponent(ShoppingDbContext db)
		{
			_db = db;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await _db.Brands.Include(x=>x.Products).ToListAsync());
		}
	}
}
