using Domain.Enums;
using MediatR;
using Domain.Entities.Sales;
using Application.Modules.Sales.DTOs;

namespace Application.Modules.Sales.Commands
{
    public record UpdateOrderCommand(
        long OrderId,
        OrderStatus newStatus,
        decimal paidAmount,
        List<UpdateOrderDetailDto> newDetails
        ) : IRequest<Unit>;
}
