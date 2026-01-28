using Domain.Entities.Sales;
using MediatR;
using Application.Interfaces.IRepository;
using Application.Modules.Sales.DTOs;

namespace Application.Modules.Sales.Queries
{
    public record GetOrderByIdQuery(long Id) : IRequest<OrderDto>;
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ISaleRepository _saleRepository;

        public GetOrderByIdQueryHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderDto = await _saleRepository.GetOrderDtoByIdAsync(request.Id);

            if (orderDto == null)
            {
                throw new KeyNotFoundException($"Order with Id {request.Id} not found.");
            }

            return orderDto;
        }
    }
}
