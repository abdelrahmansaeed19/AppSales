using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Sales
{
    public class Order
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long BranchId { get; set; }
        public long? ShiftId { get; set; }
        public long? CustomerId { get; set; }
        public OrderType OrderType { get; set; } = OrderType.DineIn;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; } = 0.00m;
        public decimal DiscountAmount { get; set; } = 0.00m;
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; } = 0.00m;
        public string? Notes { get; set; }
        public long? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
