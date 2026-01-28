using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Sales.DTOs
{
    public record OrderDetailDto(
        long Id,
        long ItemId,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice,
        string? Notes
    );
}
