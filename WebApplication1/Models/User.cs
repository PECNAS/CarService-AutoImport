using System.Security.Cryptography;
using System.Text;

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

	    public static string GetHashString(string s)
	    {
		    byte[] bytes = Encoding.Unicode.GetBytes(s);
		    MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
		    byte[] byteHash = CSP.ComputeHash(bytes);
		    string hash = "";
		    foreach (byte b in byteHash)
		    {
			    hash += string.Format("{0:x2}", b);
		    }
		    return hash;
	    }
    }


}
