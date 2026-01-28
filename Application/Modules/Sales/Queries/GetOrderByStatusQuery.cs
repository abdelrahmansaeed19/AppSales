using Application.Modules.Sales.DTOs;
using MediatR;
using Application.Interfaces.IRepository;
using Domain.Enums;


namespace Application.Modules.Sales.Queries
{
    public record GetOrderByStatusQuery(long BranchId, OrderStatus Status) : IRequest<List<OrderSummaryDto>>;

    public class GetOrderByStatusQueryHandler : IRequestHandler<GetOrderByStatusQuery, List<OrderSummaryDto>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetOrderByStatusQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<OrderSummaryDto>> Handle(GetOrderByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _saleRepository.GetOrdersByStatusAsync(request.BranchId, request.Status);

            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException($"No orders found for Branch ID: {request.BranchId} with Status: {request.Status}");
            }

            return orders;
        }

    }
}
