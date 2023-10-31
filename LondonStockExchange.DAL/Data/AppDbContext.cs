using LondonStockExchange.DAL.DataContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LondonStockExchange.DAL.Data
{
    public class AppDbContext :  IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            
        }


        public DbSet<Stock>Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Stock>()
            .Property(s => s.CurrentPrice)
            .HasColumnType("decimal(18, 2)");

            builder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Transaction>()
                .Property(t => t.SharesExchanged)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
