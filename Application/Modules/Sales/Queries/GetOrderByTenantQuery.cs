using Application.Modules.Sales.DTOs;
using MediatR;
using Application.Interfaces.IRepository;


namespace Application.Modules.Sales.Queries
{
    public record GetOrderByTenantQuery(long TenantId) : IRequest<List<OrderSummaryDto>>;

    internal class GetOrderByTenantQueryHandler : IRequestHandler<GetOrderByTenantQuery, List<OrderSummaryDto>>
    {
        private readonly ISaleRepository _saleRepository;
        public GetOrderByTenantQueryHandler(ISaleRepository orderRepository)
        {
            _saleRepository = orderRepository;
        }
        public async Task<List<OrderSummaryDto>> Handle(GetOrderByTenantQuery request, CancellationToken cancellationToken)
        {
            var orders = await _saleRepository.GetOrdersByTenantAsync(request.TenantId);

            if(orders == null || !orders.Any())
            {
                throw new KeyNotFoundException($"No orders found for TenantId: {request.TenantId}");
            }

            return orders;
        }
    }
}
