namespace WebApplication1.Models
{
	public class Order
	{
		public int Id { get; set; }
		//public List<int> ShopCartsId { get; set; }
		public virtual ICollection<ShopCart> ShopCarts { get; set; } = new List<ShopCart>();
		public string Status { get; set; } // ordered. on the way. delivered. received. cancelled
		public string Time { get; set; }
		public double Price { get; set; }
		public int PickPointId { get; set; }
		public virtual PickPoint PickPoint { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }
	}
}
