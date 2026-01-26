using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Tenants
{
    public class Tenant
    {
        public long? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Logo { get; set; }
        public string Currency { get; set; } = "EGP";
        public decimal TaxRate { get; set; } = 0.00m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
