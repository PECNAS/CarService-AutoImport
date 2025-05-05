using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Text;
using Konscious.Security.Cryptography;

namespace WebApplication1.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Role { get; set; } = "USER";
		public int PickPointId { get; set; } = 1;
		public virtual PickPoint PickPoint { get; set; }

		public virtual ICollection<ShopCart> ShopCarts { get; set; } = new List<ShopCart>();
		public virtual ICollection<Item> Favorites { get; set; } = new List<Item>();
		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();


		//
		// все для хэширования ниже
		//

		private const int _degreeOfParallelism = 8;
		private const int _iterations = 4;
		private const int _memorySize = 1024 * 1024;
		private const int _saltSize = 16;


		public static string HashPassword(string password)
		{
			var salt = GenerateSalt();

			var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
			{
				Salt = salt,
				DegreeOfParallelism = _degreeOfParallelism,
				Iterations = _iterations,
				MemorySize = _memorySize
			};

			var hash = argon2.GetBytes(32);
			return Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash);
		}

		public static bool VerifyPassword(string password, string storedHash)
		{
			var parts = storedHash.Split('|');
			if (parts.Length != 2)
				throw new ArgumentException("Недопустимый формат хэширования!", nameof(parts));

			var salt = Convert.FromBase64String(parts[0]);
			var originalHash = Convert.FromBase64String(parts[1]);

			var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
			{
				Salt = salt,
				DegreeOfParallelism = _degreeOfParallelism,
				Iterations = _iterations,
				MemorySize = _memorySize
			};

			var newHash = argon2.GetBytes(originalHash.Length);
			return CryptographicOperations.FixedTimeEquals(originalHash, newHash);
		}

		private static byte[] GenerateSalt()
		{
			using var rng = RandomNumberGenerator.Create();
			var salt = new byte[_saltSize];
			rng.GetBytes(salt);
			return salt;
		}
	}
}

