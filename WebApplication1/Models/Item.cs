namespace WebApplication1.Models
{
    public class Item
    {
        public int Id { get; set; }
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
        

    }
}
