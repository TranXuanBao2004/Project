using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Data;
using Shopping.Models;
using Shopping.Models.ViewModels;

namespace Shopping.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShoppingDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(ShoppingDbContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginVm { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await this._signInManager.PasswordSignInAsync(loginVm.UserName, loginVm.Password,false,false);
                if (result.Succeeded)
                {
                    return Redirect(loginVm.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Invaild username and password");
               
            }
            return View(loginVm);

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(AppUserVm Uservm)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = Uservm.UserName,
                    Email = Uservm.Email
                  
                };
                IdentityResult result = await this._userManager.CreateAsync(appUser,Uservm.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "Register Success";
                    return RedirectToAction("Index", "Home");
                }
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(Uservm);
        }
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await this._signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
