namespace WebApplication1.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ItemId { get; set; }
		public virtual Item Item{ get; set; }
		public int Count { get; set; }
        public bool Ordered { get; set; } = false;

        public virtual Order? Order { get; set; }

    }
}
