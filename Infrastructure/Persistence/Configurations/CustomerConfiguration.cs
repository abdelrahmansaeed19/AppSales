using Microsoft.EntityFrameworkCore;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id); // Explicitly setting Primary Key

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Phone).HasMaxLength(20).IsRequired();

            // Money precision is critical
            builder.Property(c => c.Balance).HasPrecision(18, 2);
            builder.Property(c => c.OpeningBalance).HasPrecision(18, 2);
            builder.Property(c => c.CreditLimit).HasPrecision(18, 2);

            builder.Property(c => c.Notes).HasMaxLength(500);
        }
    }
}
