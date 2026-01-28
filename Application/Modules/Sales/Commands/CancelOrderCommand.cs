using MediatR;

namespace Application.Modules.Sales.Commands
{
    public record CancelOrderCommand(
        long OrderId,
        string? Reason
        ) : IRequest<Unit>;
}
