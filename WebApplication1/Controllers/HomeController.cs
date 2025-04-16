using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public DatabaseContext db;

        public HomeController(DatabaseContext db)
        {
            this.db = db;
        }

        async public Task<IActionResult> Index()
        {
            return View();

            var user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			}
            return RedirectToAction("Index", "Catalog");
        }
    }
}
