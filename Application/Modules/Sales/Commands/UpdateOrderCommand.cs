using Domain.Enums;
using MediatR;
using Domain.Entities.Sales;

namespace Application.Modules.Sales.Commands
{
    public record UpdateOrderCommand(
        long OrderId,
        OrderStatus newStatus,
        decimal paidAmount,
        ICollection<OrderDetail> orderDetails
        ) : IRequest<Unit>;
}
