using Application.Modules.Sales.DTOs;
using MediatR;
using Application.Interfaces.IRepository;

namespace Application.Modules.Sales.Queries
{
    public record GetOrderByBranchQuery(long BranchId) : IRequest<List<OrderSummaryDto>>;

    public class GetOrderByBranchQueryHandler : IRequestHandler<GetOrderByBranchQuery, List<OrderSummaryDto>>
    {
        private readonly ISaleRepository _saleRepository;

        public GetOrderByBranchQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<List<OrderSummaryDto>> Handle(GetOrderByBranchQuery request, CancellationToken cancellationToken)
        {
            var orders = await _saleRepository.GetOrdersByBranchAsync(request.BranchId);

            if (orders == null || !orders.Any())
            {
                throw new KeyNotFoundException($"No orders found for Branch ID: {request.BranchId}");
            }

            return orders;
        }
    }
}
