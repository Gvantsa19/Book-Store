using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts.ModelBuilderExstensions;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.TablesConfigurations;

namespace ZBS.Infrastructure.DBContexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {

        }

        public DataBaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new BasketConfiguration());
            modelBuilder.ApplyConfiguration(new BasketBookConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new RatingsConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());

            modelBuilder.SeedAdmin();
            modelBuilder.SeedBookCategory();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketBook> BasketBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Ratings> Ratings { get; set; } 
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<EBooks> EBooks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sales> Sales { get; set; }

    }
}
