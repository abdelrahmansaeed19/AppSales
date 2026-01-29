using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Inventory
{
    public class Item
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long BranchId { get; set; }
        public long? CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Image { get; set; }
        public bool IsReady { get; set; } = true;
        public decimal? CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CurrentStock { get; set; } = 0.00m;
        public decimal MinStockLevel { get; set; } = 0.00m;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
