using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<PickPoint> PickPoints { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.PickPoint)
                .WithMany(p => p.Users);

            modelBuilder.Entity<Item>()
                .HasMany(i => i.InFavorites)
                .WithMany(u => u.Favorites)
                .UsingEntity(j => j.ToTable("Favorite"));

            modelBuilder.Entity<ShopCart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.ShopCarts);

            modelBuilder.Entity<ShopCart>()
                .HasOne(sc => sc.Item)
                .WithMany(i => i.ShopCarts);

            //modelBuilder.Entity<Order>()
            //    .HasMany(o => o.ShopCarts)
            //    .WithOne(sc => sc.Order);
            modelBuilder.Entity<ShopCart>()
                .HasOne(sc => sc.Order)
                .WithMany(o => o.ShopCarts);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Car)
                .WithMany(c => c.Items);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PickPoint)
                .WithMany(p => p.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders);

        }

    }
}
