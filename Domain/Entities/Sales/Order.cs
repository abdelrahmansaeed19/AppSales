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

        public static Order Create(long tenantId, long branchId, long? shiftId, long? customerId, OrderType orderType, decimal subtotal, decimal taxAmount, decimal discountAmount, decimal totalAmount, string? notes, long? createdByUserId)
        {
            return new Order
            {
                TenantId = tenantId,
                BranchId = branchId,
                ShiftId = shiftId,
                CustomerId = customerId,
                OrderType = orderType,
                Subtotal = subtotal,
                TaxAmount = taxAmount,
                DiscountAmount = discountAmount,
                TotalAmount = totalAmount,
                Notes = notes,
                CreatedByUserId = createdByUserId
            };
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Add(orderDetail);
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePaidAmount(decimal paidAmount)
        {
            PaidAmount = paidAmount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateOrderDetails(ICollection<OrderDetail> orderDetails)
        {
            OrderDetails = orderDetails;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsPaidInFull()
        {
            return PaidAmount >= TotalAmount;
        }
    }
}
