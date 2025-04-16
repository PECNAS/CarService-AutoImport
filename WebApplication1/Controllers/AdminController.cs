using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        DatabaseContext db;

        public AdminController(DatabaseContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminUsers()
        {
            var users = db.Users.ToList();
            ViewBag.users = users;
            return View();
        }

        public IActionResult AdminItems()
        {
            var items = db.Items.ToList();
            ViewBag.items = items;
            return View();
        }

        public IActionResult AdminCars()
        {
            var cars = db.Cars.ToList();
            ViewBag.cars = cars;
            return View();
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == id);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public IActionResult EditUser(EditUserModel model)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId == model.UserId);
            user.Name = model.Name;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            db.SaveChanges();

            return RedirectToAction("AdminUsers", "Admin");
        }

        [HttpGet]
        public IActionResult EditCar(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);
            ViewBag.car = car;
            return View();
        }

        [HttpPost]
        public IActionResult EditCar(EditCarModel car_model)
        {
            Car car = db.Cars.FirstOrDefault(c => c.Id == car_model.Id);
            car.Mark = car_model.Mark;
            car.Model = car_model.Model;
            car.Year = car_model.Year;
            db.SaveChanges();

            return RedirectToAction("AdminCars", "Admin");
        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var item = db.Items.FirstOrDefault(i => i.Id== id);
            ViewBag.item = item;
            return View();
        }

        [HttpPost]
        public IActionResult EditItem(EditItemModel model)
        {
            Item item = db.Items.FirstOrDefault(i => i.Id == model.Id);
            item.Title = model.Title;
            item.Description = model.Description;
            item.Price = model.Price;
            item.CarId = model.CarId;
            item.Seller = model.Seller;

            db.SaveChanges();

            return RedirectToAction("AdminItems", "Admin");
        }
    }
}
