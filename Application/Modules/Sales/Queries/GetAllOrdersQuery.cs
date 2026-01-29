using MediatR;
using Application.Modules.Sales.DTOs;
using System.Collections.Generic;

namespace Application.Modules.Sales.Queries
{
    public record GetAllOrdersQuery() : IRequest<List<OrderSummaryDto>>;
}
