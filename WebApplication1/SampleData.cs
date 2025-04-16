using WebApplication1.Models;

namespace WebApplication1
{
	public class SampleData
	{
		public static void Initialize(DatabaseContext context, IWebHostEnvironment env)
		{
			if (!context.Cars.Any())
			{
				context.Cars.AddRange(
					new Car
					{
						Mark = "Lada",
						Model = "Granta",
						Year = "2010"
					},
					new Car
					{
						Mark = "Wolksvagen",
						Model = "Polo",
						Year = "2012"
					},
					new Car
					{
						Mark = "Lada",
						Model = "2114",
						Year = "2005"
					},
					new Car
					{
						Mark = "Chevrolette",
						Model = "Niva",
						Year = "2016"
					}
				);
				context.SaveChanges();
			}

			if (!context.Categories.Any())
			{
				context.Categories.AddRange(
					new Category
					{
						Title = "Колодки"
					},
					new Category
					{
						Title = "Фары"
					},
					new Category
					{
						Title = "Шины"
					}
				);
				context.SaveChanges();
			}

			if (!context.Items.Any())
			{
				context.Items.AddRange(
					new Item
					{
						Title = "Фара",
						Type = "Неоновая",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 500,
						Image = "item1.jpg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 1
					},
					new Item
					{
						Title = "Ступечные подшипники",
						Type = "Размер: 44х35х60",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 8000,
						Image = "item.png",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 2
					},
					new Item
					{
						Title = "Блок управления реле",
						Type = "Блок реле 2098672ё",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 3000,
						Image = "item3.jpeg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 3
					},
					new Item
					{
						Title = "Фара",
						Type = "Неоновая",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 500,
						Image = "item1.jpg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 1
					},
					new Item
					{
						Title = "Ступечные подшипники",
						Type = "Размер: 44х35х60",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 8000,
						Image = "item.png",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 2
					},
					new Item
					{
						Title = "Блок управления реле",
						Type = "Блок реле 2098672ё",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 3000,
						Image = "item3.jpeg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 3
					},
					new Item
					{
						Title = "Фара",
						Type = "Неоновая",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 500,
						Image = "item1.jpg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 1
					},
					new Item
					{
						Title = "Ступечные подшипники",
						Type = "Размер: 44х35х60",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 8000,
						Image = "item.png",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 2
					},
					new Item
					{
						Title = "Блок управления реле",
						Type = "Блок реле 2098672ё",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 3000,
						Image = "item3.jpeg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 3
					},
					new Item
					{
						Title = "Фара",
						Type = "Неоновая",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 500,
						Image = "item1.jpg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 1
					},
					new Item
					{
						Title = "Ступечные подшипники",
						Type = "Размер: 44х35х60",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 8000,
						Image = "item.png",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 2
					},
					new Item
					{
						Title = "Блок управления реле",
						Type = "Блок реле 2098672ё",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 3000,
						Image = "item3.jpeg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 3
					},
					new Item
					{
						Title = "Фара",
						Type = "Неоновая",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 500,
						Image = "item1.jpg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 1
					},
					new Item
					{
						Title = "Ступечные подшипники",
						Type = "Размер: 44х35х60",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 8000,
						Image = "item.png",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 2
					},
					new Item
					{
						Title = "Блок управления реле",
						Type = "Блок реле 2098672ё",
						Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ut nulla sit dolorum iste veniam est cupiditate aspernatur aut quae corrupti optio, tempore minima consectetur tempora doloribus, dolore in, consequatur veritatis, sunt id voluptate voluptas qui. Qui maiores inventore, provident dolorum deleniti. Odio vitae molestiae eveniet, suscipit quidem id dolor ducimus. Sapiente veritatis, sed facere quam laboriosam velit eaque nisi deserunt hic, porro quidem vero cum, repellendus voluptates ab odit reiciendis.",
						Price = 3000,
						Image = "item3.jpeg",
						CarId = 3,
						Seller = "ДоброДетАЛЬ",
						Rating = 4.9f,
						CategoryId = 3
					}
				);
				context.SaveChanges();
			}

			if (!context.PickPoints.Any())
			{
				context.PickPoints.AddRange(
					new PickPoint
					{
						Adress = "г. Казань, ул. Островского, д. 8а",
						WorkTime = "8.00 - 20.00"
					},
					new PickPoint
					{
						Adress = "г. Казань, ул. Аделя Кутуая, д. 21",
						WorkTime = "8.00 - 18.00"
					},
					new PickPoint
					{
						Adress = "г. Казань, ул. Мира, д. 44",
						WorkTime = "10.00 - 20.00"
					},
					new PickPoint
					{
						Adress = "г. Казань, ул. Михаила Миля, д. 12д",
						WorkTime = "10.00 - 20.00"
					}
				);

				context.SaveChanges();
			}
		}
	}
}
