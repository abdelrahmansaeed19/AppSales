using System;
using System.Collections.Generic;
using System.Text;

namespace App_Sales.Models.Inventory
{
    public class Material
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Unit { get; set; } = string.Empty; 
        public decimal CurrentQuantity { get; set; } = 0.00m;
        public decimal MinQuantity { get; set; } = 0.00m;
        public decimal? CostPerUnit { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
