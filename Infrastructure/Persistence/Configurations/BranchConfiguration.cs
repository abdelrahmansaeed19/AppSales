using Microsoft.EntityFrameworkCore;
using Domain.Entities.Tenants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
            builder.Property(b => b.Email).HasMaxLength(255);
            builder.Property(b => b.Phone).HasMaxLength(50);
            builder.Property(b => b.Address).HasColumnType("text");

            builder.Property(b => b.IsActive).HasDefaultValue(true);
            builder.Property(b => b.CreatedAt).HasColumnType("datetime");
            builder.Property(b => b.UpdatedAt).HasColumnType("datetime");

            builder.HasOne(b => b.Tenant)
                   .WithMany()
                   .HasForeignKey(b => b.TenantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
