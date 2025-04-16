using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CatalogController : Controller
    {
        DatabaseContext db;

        public CatalogController(DatabaseContext db)
        {
            this.db = db;
        }

        public IActionResult Index(List<Item> items = null)
        {
            List<Category> categories = db.Categories.ToList();
            ViewBag.categories = categories;
            if (items == null)
            {
                items = db.Items.ToList();
            }

			List<Item> DOWN2UP_PRICE = items.OrderBy(i => i.Price).ToList();
            List<Item> UP2DOWN_PRICE = items.OrderByDescending(i => i.Price).ToList();
			List<Item> UP2DOWN_TITLE = items.OrderBy(i => i.Title).ToList();
            List<Item> DOWN2UP_TITLE = items.OrderByDescending(i => i.Title).ToList();

			ViewBag.items = items;
            ViewBag.UP2DOWN_PRICE = UP2DOWN_PRICE;
            ViewBag.UP2DOWN_TITLE = UP2DOWN_TITLE;
            ViewBag.DOWN2UP_PRICE = DOWN2UP_PRICE;
            ViewBag.DOWN2UP_TITLE = DOWN2UP_TITLE;


            List<int> favs_bag = new List<int>();
            List<int> carts_bag = new List<int>();
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (user != null)
            {
                favs_bag = (from f in db.Favorites
                            where f.UserId == user.UserId
                            select f.ItemId).ToList();

                carts_bag = (from c in db.ShopCarts
                             where c.UserId == user.UserId &&
                             c.Ordered == false
                             select c.ItemId).ToList();
            }
            ViewBag.favs = favs_bag;
            ViewBag.carts = carts_bag;

            return View("ItemsCatalog");
        }

        [HttpGet]
        public IActionResult ItemDetail(int id)
        {
            var item = db.Items.FirstOrDefault(i => i.Id == id);
            ViewBag.item = item;

            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(BuyModel model)
        {
            int id = model.ItemId;
            int count = model.Counter;

            var item = db.Items.FirstOrDefault(i => i.Id == id);
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            if (user != null)
            {
                ShopCart sc = db.ShopCarts.FirstOrDefault(sc => sc.Item == item &&  sc.User == user && sc.Ordered == false);

                if (sc == null)
                {
                    ShopCart new_cart = new ShopCart
                    {
                        User = user,
                        Item = item,
                        Count = count
                    };

                    db.ShopCarts.Add(new_cart);

                }
                else sc.Count += count;

                db.SaveChanges();

                return new NoContentResult();
                return RedirectToAction("MyCart", "Catalog");
            }
            return RedirectToAction("Login", "User");
        }

        public IActionResult RemoveFromCart(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            ShopCart sc = db.ShopCarts.FirstOrDefault(sc => sc.User == user && sc.ItemId == id && sc.Ordered == false);
            db.ShopCarts.Remove(sc);
            db.SaveChanges();

            return RedirectToAction("MyCart");

        }

        [HttpGet]
        public IActionResult MyCart()
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "User");
            }

            var carts = db.ShopCarts.Where(c => c.User == user && c.Ordered == false).ToList();
            List<Item> items = new List<Item>();
            List<int> counts = new List<int>();

            double full_price = 0;
            foreach (var cart in carts)
            {
                Item item = db.Items.FirstOrDefault(i => i.Id == cart.ItemId);
                items.Add(item);
                counts.Add(cart.Count);

                full_price += item.Price * cart.Count;
            }

            ViewBag.items = items;
            ViewBag.counts = counts;
            ViewBag.full_price = full_price;
            ViewBag.points = db.PickPoints.ToList();

            return View(); 
        }

        [HttpPost]
        public IActionResult Finder(string query)
        {
            if (query != null)
            {
                List<Item> items = db.Items.Where(i => i.Title
                .ToLower()
                .Contains(
                    query.ToLower())
                ).ToList();
                return Index(items);
            } else
            {
                return new NoContentResult();
            }

            //return PartialView("ItemsCatalog", items);
        }

        [HttpPost]
        public IActionResult CategoryFilter(int CategoryId)
        {
            List<Item> items = new List<Item>();
            if (CategoryId == -1)
            {
				items = db.Items.ToList();
			} else
            {
                items = db.Items.Where(i => i.CategoryId == CategoryId).ToList();
            }
            return Index(items);
        }



        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderModel model)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            var carts = db.ShopCarts.Where(sc => sc.User == user && sc.Ordered == false).ToList();
            PickPoint pickPoint;

            if (model.MyAdress == null && model.PickPointId != null)
            {
                pickPoint = db.PickPoints.FirstOrDefault(p => p.Id == model.PickPointId);
			} else
            {
				pickPoint = new PickPoint()
				{
					Adress = model.MyAdress,
					WorkTime = "",
					IndividualUserId = user.UserId
				};
                db.PickPoints.Add(pickPoint);
                db.SaveChanges();
			}

			Order new_order = new Order()
            {
                Time = DateTime.Now.ToString(),
                Status = "Ordered",
                Price = model.Price,
                PickPoint = pickPoint,
                ShopCarts = carts.ToList(),
                User = user
            };
            db.Orders.Add(new_order);
            db.SaveChanges();

            foreach (var cart in carts) {
                cart.Ordered = true;
                cart.Order = new_order;
            }

            db.SaveChanges();

			return RedirectToAction("Index", "User");
		}

        [HttpPost]
        public IActionResult Favorites(int ItemId, string NeedToRedirect)
        {
			User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
			if (user == null)
			{
				return RedirectToAction("Login", "User");
			}


            var item = db.Items.First(i => i.Id == ItemId);
            if (db.Favorites.FirstOrDefault(f => f.UserId == user.UserId && f.ItemId == item.Id) == null)
            {
                db.Favorites.Add(new Favorite()
                {
                    UserId = user.UserId,
                    ItemId = item.Id
                });

                db.SaveChanges();
            }
            
            return new NoContentResult();
		}

		[HttpPost]
		public IActionResult RemoveFavorites(int ItemId, string NeedToRedirect)
		{
			User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
			var item = db.Items.First(i => i.Id == ItemId);

            var fav = db.Favorites.FirstOrDefault(f => f.UserId == user.UserId && f.ItemId == item.Id);
            db.Favorites.Remove(fav);
            db.SaveChanges();

            Console.WriteLine($"\n\n\n{NeedToRedirect}\n\n\n");
            if (NeedToRedirect == "true")
                return RedirectToAction("Favorites", "User");
            else
                return new NoContentResult();
		}
	}
}
