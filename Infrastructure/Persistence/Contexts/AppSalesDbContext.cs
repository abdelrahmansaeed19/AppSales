using Domain.Entities.Auth;
using Domain.Entities.Customers;
using Domain.Entities.Expenses;
using Domain.Entities.Inventory;
using Domain.Entities.Sales;
using Domain.Entities.Tenants;
using Domain.Entities.Users;
//using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public class AppSalesDbContext : DbContext
    {
        public AppSalesDbContext(DbContextOptions<AppSalesDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await base.SaveChangesAsync(cancellationToken);
        //}
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<EmailVerificationCode> EmailVerificationCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppSalesDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>()
                .Property(t => t.TaxRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>(); // Save Enum as String in DB

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Subtotal).HasPrecision(10, 2);
                entity.Property(e => e.TaxAmount).HasPrecision(10, 2);
                entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
                entity.Property(e => e.PaidAmount).HasPrecision(10, 2);

                entity.Property(e => e.OrderType).HasConversion<string>();
                entity.Property(e => e.Status).HasConversion<string>();
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Quantity).HasPrecision(10, 2);
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
                entity.Property(e => e.TotalPrice).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.CostPrice).HasPrecision(10, 2);
                entity.Property(e => e.SellingPrice).HasPrecision(10, 2);
                entity.Property(e => e.CurrentStock).HasPrecision(10, 2);
                entity.Property(e => e.MinStockLevel).HasPrecision(10, 2);
                entity.HasIndex(e => e.Sku).IsUnique(); // Unique SKU [cite: 486]
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.CurrentQuantity).HasPrecision(10, 2);
                entity.Property(e => e.MinQuantity).HasPrecision(10, 2);
                entity.Property(e => e.CostPerUnit).HasPrecision(10, 2);

                // Optional: Map property to specific column name or constraints if needed
                entity.Property(e => e.Unit).HasMaxLength(50).IsRequired();
            });
        }
    }
}