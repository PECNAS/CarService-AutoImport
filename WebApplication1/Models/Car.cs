namespace WebApplication1.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
