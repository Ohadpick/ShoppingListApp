using ShoppingListApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            
        }
    }
}