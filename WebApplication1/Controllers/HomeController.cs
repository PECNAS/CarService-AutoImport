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

        public IActionResult Index()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.error_message = TempData["Error"];
                ViewBag.IsValid = false;
            }

            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            ViewBag.authorized = user != null;
            return View();
             
            
        }
    }
}
