using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        DatabaseContext db;
        public UserController(DatabaseContext db)
        {
            this.db = db;
        }

        async public Task<IActionResult> Index()
        {   
            var user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                TempData["error"] = "Вы не авторизованы в системе!";
                return RedirectToAction("Index", "Home");
				
			}

            List<List<Item>> orders = new List<List<Item>>();
            List<int> counts = new List<int>();
            List<Dictionary<string, string>> orders_info = new List<Dictionary<string, string>>();

            var db_orders = db.Orders.Where(o => o.User == user).OrderByDescending(o => o.Time).ToList();
            foreach (Order order in db_orders)
            {
                PickPoint point = db.PickPoints.First(p => p.Id == order.PickPointId);
				List<Item> items = new List<Item>();
                Dictionary<string, string> info = new Dictionary<string, string>
                {
                    { "Status", order.Status.ToString() },
                    { "Time", order.Time.ToString() },
                    { "Price", order.Price.ToString() },
                    { "PointAdress", point.Adress },
                    { "PointWorkTime", point.WorkTime },
                };

                var carts = db.ShopCarts.Where(sc => sc.Order == order).ToList();
                foreach (ShopCart cart in carts) {
                    Console.WriteLine($"\n\n\n\n{cart.Count}\n\n");
                    counts.Add(cart.Count);
                    items.Add(db.Items.First(i => i.Id == cart.ItemId));
                }
                orders.Add( items );
                orders_info.Add( info );
            }

			ViewBag.user = user;
			ViewBag.orders = orders;
            ViewBag.counts = counts;
            ViewBag.orders_info = orders_info;
            ViewBag.point = db.PickPoints.First(p => p.Id == db_orders.First().PickPointId);

            return View("Profile");
        }

        [HttpGet]
        public IActionResult Favorites()
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
				TempData["error"] = "Вы не авторизованы в системе!";
				return RedirectToAction("Index", "Home");
			}

            var favs = db.Favorites.Where(f => f.UserId == user.UserId).ToList();
            List<Item> items = new List<Item>();

            foreach (var fav in favs)
            {
                var item = db.Items.First(i => i.Id == fav.ItemId);

				items.Add(item);
            }
			var carts_bag = (from c in db.ShopCarts
						 where c.UserId == user.UserId &&
						 c.Ordered == false
						 select c.ItemId).ToList();
			ViewBag.carts = carts_bag;
			ViewBag.items = items;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password)
        {
			LoginModel model = new LoginModel();
            model.Email = Email;
            model.Password = Password;

			if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    if (Models.User.VerifyPassword(model.Password, user.Password))
                    {
                        await Authenticate(model.Email); // аутентификация
                        ViewBag.authorized = true;
                        return RedirectToAction("Index", "Home");
                    }
                }
                string error = "";
                user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null) error = "Неверный пароль!";
                else error = "Пользователь с почтой " + model.Email + " не найден";

                //ModelState.AddModelError("Email", error);
                ViewBag.error_message = error;
            }
			ViewBag.IsValid = false;
			return View("~/Views/Home/Index.cshtml", model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string Password, string Email, string PhoneNumber, string Name)
        {
			RegModel model = new RegModel();
            model.PhoneNumber = PhoneNumber;
            model.Name = Name;
            model.Password = Password;
            model.Email = Email;

			if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.Users.Add(
                        new User {
                            Email = model.Email,
                            Password = Models.User.HashPassword(model.Password),
                            PhoneNumber = model.PhoneNumber,
                            Name = model.Name
                        });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string error = "";

                    user = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
                    if (user != null) error = "Номер телефона уже занят";

                    user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user != null) error = "Почта уже занята";

                    //ModelState.AddModelError("Email", error);
                    ViewBag.error_message = error;
                }
            }
            ViewBag.IsValid = false;
            return View("~/Views/Home/Index.cshtml", model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
