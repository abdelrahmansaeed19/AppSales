using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);

            // Enum stored as string for readability (Optional, but recommended)
            builder.Property(u => u.Role).HasConversion<string>();

            // Relationships
            builder.HasOne(u => u.Tenant)
                   .WithMany() // Assuming Tenant has a collection of Users, or define WithMany(t => t.Users)
                   .HasForeignKey(u => u.TenantId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent deleting Tenant if users exist

            builder.HasOne(u => u.Branch)
                   .WithMany()
                   .HasForeignKey(u => u.BranchId)
                   .IsRequired(false) // Branch is optional
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
