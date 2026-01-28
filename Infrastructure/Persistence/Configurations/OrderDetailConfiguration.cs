using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            // Primary Key
            builder.HasKey(od => od.Id);

            // Financial Precision

            builder.Property(od => od.Quantity).HasPrecision(12, 3);

            builder.Property(od => od.UnitPrice).HasPrecision(18, 2);
            builder.Property(od => od.TotalPrice).HasPrecision(18, 2);

            builder.Property(od => od.Notes).HasMaxLength(255);

            // Relationships

            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId);

            // Important: Link to Item (Product)
            // Assuming you have an 'Item' entity in Inventory module
            /*
            builder.HasOne<Domain.Entities.Inventory.Item>()
                   .WithMany()
                   .HasForeignKey(od => od.ItemId)
                   .OnDelete(DeleteBehavior.Restrict);
            */
        }
    }
}
