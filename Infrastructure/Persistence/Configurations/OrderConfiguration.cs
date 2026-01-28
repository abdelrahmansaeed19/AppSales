using Microsoft.EntityFrameworkCore;
using Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.Tenants;
using Domain.Entities.Users;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties
            builder.Property(o => o.OrderType)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Status)
                .HasConversion<string>() 
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Notes)
                .HasMaxLength(500);

            builder.Property(o => o.Subtotal).HasPrecision(18, 2);
            builder.Property(o => o.TaxAmount).HasPrecision(18, 2);
            builder.Property(o => o.DiscountAmount).HasPrecision(18, 2);
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2);
            builder.Property(o => o.PaidAmount).HasPrecision(18, 2);

            builder.HasMany(o => o.OrderDetails)
                   .WithOne(od => od.Order)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Tenant>()
                   .WithMany()
                   .HasForeignKey(o => o.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Branch>()
                   .WithMany()
                   .HasForeignKey(o => o.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(o => o.CreatedByUserId)
                   .OnDelete(DeleteBehavior.SetNull); // Keep order history even if user is deleted
        }
    }
}
