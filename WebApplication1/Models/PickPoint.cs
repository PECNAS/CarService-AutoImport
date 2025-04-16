namespace WebApplication1.Models
{
	public class PickPoint
	{
		public int Id { get; set; }
		public string Adress { get; set; }
		public string WorkTime { get; set; }
		public int? IndividualUserId { get; set; }

		public virtual ICollection<User> Users { get; set; } = new List<User>();
		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

	}
}
