using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Sales
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ItemId { get; set; }
        public long? ItemVariantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Order Order { get; set; } = null!;
    }
}
