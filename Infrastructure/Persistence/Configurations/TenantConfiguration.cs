using Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.Id); 

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(t => t.Email).IsUnique();

            builder.Property(t => t.Phone)
                .HasMaxLength(50); 

            builder.Property(t => t.Address)
                .HasColumnType("text");

            builder.Property(t => t.Logo)
                .HasMaxLength(255);

            builder.Property(t => t.Currency)
                .HasMaxLength(3)
                .HasDefaultValue("EGP");

            builder.Property(t => t.TaxRate)
                .HasPrecision(5, 2)      
                .HasDefaultValue(0.00m);

            builder.Property(t => t.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(t => t.UpdatedAt)
                .HasColumnType("datetime");
        }
    }
}
