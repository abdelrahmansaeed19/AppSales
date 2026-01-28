using Application.Interfaces.IRepository;
using MediatR;
using Application.Modules.Sales.Commands;
using Domain.Entities.Sales;
using Domain.Enums;
using Application.Modules.Sales.DTOs;

namespace Application.Modules.Sales.Handler
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateOrderCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        private void UpdateOrderDetails(Order order, List<UpdateOrderDetailDto> newDetails)
        {
            var detailsToRemove = order.OrderDetails
                .Where(od => !newDetails.Any(nd => nd.Id == od.Id && od.Id != 0))
                .ToList();

            foreach (var detail in detailsToRemove)
            {
                order.OrderDetails.Remove(detail);
            }

            foreach (var newDetail in newDetails)
            {
                var existingDetail = order.OrderDetails
                    .FirstOrDefault(od => od.Id == newDetail.Id && od.Id != 0);


                if (existingDetail != null)
                {
                    if (existingDetail.Quantity != newDetail.Quantity)
                    {
                        existingDetail.UpdateQuantity(newDetail.Quantity);
                    }
                    if (existingDetail.UnitPrice != newDetail.UnitPrice)
                    {
                        existingDetail.UpdateUnitPrice(newDetail.UnitPrice);
                    }
                    if (!string.Equals(existingDetail.Notes, newDetail.Notes, StringComparison.Ordinal))
                    {
                        existingDetail.AddNotes(newDetail.Notes ?? string.Empty);
                    }
                }
                else
                {
                    var detailToAdd = OrderDetail.Create(
                        order.Id,
                        newDetail.ItemId,
                        newDetail.ItemVariantId,
                        newDetail.Quantity,
                        newDetail.UnitPrice,
                        newDetail.Notes);


                    order.AddOrderDetail(detailToAdd);
                }
            }
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _saleRepository.GetOrderByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            if(request.newStatus == OrderStatus.Cancelled)
            {
                throw new Exception("Order cancellation is not allowed through this operation");
            }

            if (order.Status != request.newStatus)
            {
                order.UpdateStatus(request.newStatus);
            }

            if(order.PaidAmount != request.paidAmount)
            {
                order.UpdatePaidAmount(request.paidAmount);
            }

            UpdateOrderDetails(order, request.newDetails);

            await _saleRepository.UpdateOrderAsync(order);

            return Unit.Value;
        }
    }
}
