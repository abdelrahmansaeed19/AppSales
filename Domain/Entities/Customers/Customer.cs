namespace Domain.Entities.Customers
{
    public class Customer
    {
        // Primary Key (Since no BaseEntity)
        public long Id { get; set; }

        // Basic Info
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }

        // Financial Data
        public decimal Balance { get; private set; } = 0; // The calculated current balance
        public decimal OpeningBalance { get; set; } = 0;  // From "Financial Data" tab
        public decimal? CreditLimit { get; set; }         // From "Financial Data" tab

        // Meta Data
        public string? Notes { get; set; }

        // Multi-tenancy
        public int TenantId { get; set; }

        // Audit (Optional since no BaseEntity)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Logic
        public void AddTransaction(decimal amount)
        {
            Balance += amount;
        }
    }

}