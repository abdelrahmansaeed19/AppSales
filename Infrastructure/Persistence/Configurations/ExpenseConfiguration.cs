using Microsoft.EntityFrameworkCore;
using Domain.Entities.Expenses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Amount).HasPrecision(18, 2);
            builder.Property(e => e.Category).HasMaxLength(50);
            builder.Property(e => e.AttachmentUrl).HasMaxLength(2048); // Standard URL length
        }
    }
}
