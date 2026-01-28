using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Sales.DTOs
{
    public record OrderSummaryDto(
        long Id,
        long BranchId,
        string OrderType,
        string Status,
        decimal TotalAmount,
        int ItemCount,
        DateTime CreatedAt
    );
}
