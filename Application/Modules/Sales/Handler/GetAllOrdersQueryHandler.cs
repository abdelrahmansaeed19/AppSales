using Application.Interfaces.IRepository;
using Application.Modules.Sales.DTOs;
using Application.Modules.Sales.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Sales.Handler
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderSummaryDto>>
    {
        private readonly ISaleRepository _repository;

        public GetAllOrdersQueryHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderSummaryDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllOrdersAsync();

            // Map to OrderSummaryDto
            return orders
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OrderSummaryDto(
                    o.Id,
                    o.BranchId,
                    o.CustomerId,
                    o.OrderType.ToString(),
                    o.Status.ToString(),
                    o.TotalAmount,
                    o.OrderDetails.Count,
                    o.CreatedAt
                ))
                .ToList();
        }
    }

}
