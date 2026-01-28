using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Sales.DTOs
{
    public record UpdateOrderDetailDto
    (
        long Id,
        long ItemId,
        long? ItemVariantId,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice,
        string? Notes
    );
}
