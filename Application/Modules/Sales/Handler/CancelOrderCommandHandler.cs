using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.IRepository;
using MediatR;
using Application.Modules.Sales.Commands;
using Domain.Enums;

namespace Application.Modules.Sales.Handler
{
    public class CancelOrderCommandHandler
    {
        private readonly ISaleRepository _saleRepository;

        public CancelOrderCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _saleRepository.GetOrderByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {request.OrderId} not found.");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException($"Order with ID {request.OrderId} is already canceled.");
            }

            if (order.Status == OrderStatus.Completed)
            {
                throw new InvalidOperationException($"Completed order with ID {request.OrderId} cannot be canceled.");
            }

            order.UpdateStatus(OrderStatus.Cancelled);

            if (!string.IsNullOrWhiteSpace(request.Reason))
            {
                string noteAppend = $"\nCancellation Reason: {request.Reason}";

                if(string.IsNullOrWhiteSpace(order.Notes))
                {
                    order.Notes = noteAppend.Trim();
                }
                else
                {
                    order.Notes += noteAppend;
                }
            }

            await _saleRepository.UpdateOrderAsync(order);

            return Unit.Value;
        }
    }
}
