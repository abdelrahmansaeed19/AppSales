using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Sales.DTOs
{
    public record OrderDto(
        long Id,
        long TenantId,
        long BranchId,
        string OrderType,
        string Status,
        decimal Subtotal,
        decimal TaxAmount,
        decimal DiscountAmount,
        decimal TotalAmount,
        decimal PaidAmount,
        string? Notes,
        DateTime CreatedAt,
        List<OrderDetailDto> Items
    );
}
