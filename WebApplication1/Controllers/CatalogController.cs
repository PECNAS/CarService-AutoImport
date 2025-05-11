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
        private const int DISTANCE = 2;

        public CatalogController(DatabaseContext db)
        {
            this.db = db;
        }

        public IActionResult Index(List<Item> items = null, int? car_id = null)
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

            var cars = db.Cars.ToList();
            ViewBag.cars = cars;

            if (car_id != null)
            {
                ViewBag.car_id = car_id;
            }

            return View("ItemsCatalog");
        }

        [HttpGet]
        public IActionResult ItemDetail(int id)
        {
            var item = db.Items.FirstOrDefault(i => i.Id == id);
            ViewBag.item = item;

            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user != null)
            {
                var fav = db.Favorites.FirstOrDefault(f => f.UserId == user.UserId && f.ItemId == item.Id);
                if (fav != null)
                {
                    ViewBag.fav = true;
                }
                else
                {
                    ViewBag.fav = false;
                }
            }
            else
            {
                ViewBag.fav = null;
            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangeCart(int item_id, int val)
        {
            User user = db.Users.First(u => u.Email == User.Identity.Name);
            ShopCart sc = db.ShopCarts.FirstOrDefault(sc => sc.ItemId == item_id && sc.UserId == user.UserId && sc.Ordered == false);
            Console.Write($"\n\n\n{item_id}\n\n{val}\n\n\n\n");

            if (sc != null)
            {
                Item item = db.Items.First(i => i.Id == item_id);
                if (sc.Count + val <= item.Count && sc.Count + val > 0)
                {
                    Console.Write($"\n\n\n {sc.Count} \n\n\n");
                    sc.Count += val;
                }
            }
            db.SaveChanges();
            return new NoContentResult();
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
                ShopCart sc = db.ShopCarts.FirstOrDefault(sc => sc.Item == item && sc.User == user && sc.Ordered == false);

                if (sc == null)
                {
                    if (count > item.Count) count = item.Count; // если пытаемся добавить в корзину больше товаров, чем есть - ограничиваем

                    ShopCart new_cart = new ShopCart
                    {
                        User = user,
                        Item = item,
                        Count = count
                    };

                    db.ShopCarts.Add(new_cart);

                }
                else
                {
                    if (sc.Count + count <= item.Count) sc.Count += count;
                }

                db.SaveChanges();

                return new NoContentResult();
                return RedirectToAction("MyCart", "Catalog");
            }

			TempData["error"] = "Вы не авторизованы в системе!";
			return RedirectToAction("Index", "Home");
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

				TempData["error"] = "Вы не авторизованы в системе!";
				return RedirectToAction("Index", "Home");
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
            ViewBag.points = db.PickPoints.Where(p => p.IndividualUserId == user.UserId || p.IndividualUserId == null).ToList();

            return View(); 
        }

        [HttpPost]
        public IActionResult Finder(string query)
        {
            if (query != null)
            {
                List<Item> items = db.Items.Where(i => i.Title
                
                .Contains(
                    query)
                ).ToList();
                return Index(items);
            } else
            {
                return new NoContentResult();
            }

            //return PartialView("ItemsCatalog", items);
        }

        [HttpGet]
        public IActionResult CarFilter(int car_id)
        {
            List<Item> items = new List<Item>();

            if (car_id == -1)
            {
                items = db.Items.ToList();
            } else
            {
                items = db.Items.Where(i => i.CarId == car_id).ToList();
            }

            return Index(items, car_id);
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
                Status = "ORDERED",
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

                Item item = db.Items.First(i => i.Id == cart.ItemId);
                item.Count -= cart.Count;
                db.SaveChanges();
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

				TempData["error"] = "Вы не авторизованы в системе!";
				return RedirectToAction("Index", "Home");
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
            try
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
            catch
            {
				return new NoContentResult();
			}
		}

        [HttpGet]
        public object AsyncFinder(string query)
        {
			var res_items = new List<Item>();
            if (query != null)
            {
                query = query.ToLower();
                var items = db.Items.ToList();

                foreach(var item in items)
                {
                    if (Item.TitleCompare(query, item.Title.ToLower()) <= DISTANCE ||
                            item.Title
                            .ToLower()
                            .Split(" ")
                            .Any(s => s.Contains(query) || Item.TitleCompare(query, s) <= DISTANCE)
                        )
                    {
					    if (res_items.Where(i => i.Title == item.Title).Count() == 0)
                            res_items.Add(item);
                    }
                }
			}

            return new { Items = res_items};
        }

		[HttpGet]
		public IActionResult RedirectToQuery(string query)
		{
			if (query != null)
			{
				List<Item> items = db.Items.Where(i => i.Title
				
				.Contains(
					query)
				).ToList();
				return Index(items);
			}
			else
			{
				return new NoContentResult();
			}

			//return PartialView("ItemsCatalog", items);
		}
	}
}
