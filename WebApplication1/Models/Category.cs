namespace WebApplication1.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public virtual ICollection<Item> Items { get; set; } = new List<Item>();
	}
}
