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
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Order Order { get; set; } = null!;


        public static OrderDetail Create(long orderId, long itemId, long? itemVariantId, int quantity, decimal unitPrice, string? notes = null)
        {
            var totalPrice = quantity * unitPrice;
            return new OrderDetail
            {
                OrderId = orderId,
                ItemId = itemId,
                ItemVariantId = itemVariantId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice,
                Notes = notes,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
            TotalPrice = Quantity * UnitPrice;
        }

        public void UpdateUnitPrice(decimal newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            TotalPrice = Quantity * UnitPrice;
        }

        public void AddNotes(string additionalNotes)
        {
            if (string.IsNullOrEmpty(Notes))
            {
                Notes = additionalNotes;
            }
            else
            {
                Notes += " " + additionalNotes;
            }
        }
    }
}
