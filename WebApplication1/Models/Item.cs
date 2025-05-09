using System.Runtime.CompilerServices;

namespace WebApplication1.Models
{
    public class Item
    {
        public int Id { get; set; }
		public int Count { get; set; } = 0;
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public string Seller { get; set; }
        public float Rating { get; set; } = 0f;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ShopCart> ShopCarts { get; set; } = new List<ShopCart>();
        public virtual ICollection<User> InFavorites { get; set; } = new List<User>();


		public static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

		public static int TitleCompare(string query, string title)
		{
			string firstWord = query.ToLower();
			string secondWord = title.ToLower();
			var n = firstWord.Length + 1;
			var m = secondWord.Length + 1;
			var matrixD = new int[n, m];

			const int deletionCost = 1;
			const int insertionCost = 1;

			for (var i = 0; i < n; i++)
			{
				matrixD[i, 0] = i;
			}

			for (var j = 0; j < m; j++)
			{
				matrixD[0, j] = j;
			}

			for (var i = 1; i < n; i++)
			{
				for (var j = 1; j < m; j++)
				{
					var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

					matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
											matrixD[i, j - 1] + insertionCost,         // вставка
											matrixD[i - 1, j - 1] + substitutionCost); // замена
				}
			}

			return matrixD[n - 1, m - 1];
		}
	}
}
