using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Modules.Sales.DTOs;
using Domain.Entities.Sales;
using Domain.Enums;
using MediatR;

namespace Application.Modules.Sales.Commands
{
    public record CreateOrderCommand(
        long TenantId,
        long BranchId,
        long ShiftId,
        long CustomerId,
        OrderType OrderType,
        OrderStatus Status,
        decimal Subtotal,
        decimal TaxAmount,
        decimal DiscountAmount,
        decimal TotalAmount,
        decimal PaidAmount,
        string? Notes,
        long CreatedByUserId,
        List<OrderDetailDto> Items
        ) : IRequest<long>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, long>
    {
        private readonly ISaleRepository _salesRepository;

        public CreateOrderCommandHandler(ISaleRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<long> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            if (request.TotalAmount < 0)
            {
                throw new ArgumentException("Total amount cannot be negative.");
            }

            var ItemIds = request.Items.Select(i => i.ItemId).ToList();

            var Items = await _salesRepository.GetItemsByIdsInBranchAsync(ItemIds, request.BranchId);

            var dbItemsMap = Items.ToDictionary(x => x.Id);

            foreach (var item in request.Items)
            {
                if(!dbItemsMap.TryGetValue(item.ItemId, out var dbItem))
                {
                    throw new NotFoundException($"Item with ID {item.ItemId} not found in branch {request.BranchId}.");
                }

                if(dbItem.CurrentStock < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for item ID {item.ItemId}. Available: {dbItem.CurrentStock}, Requested: {item.Quantity}.");
                }

                dbItem.CurrentStock -= item.Quantity;
            }

            Order order = Order.Create(
                tenantId: request.TenantId,
                branchId: request.BranchId,
                shiftId: request.ShiftId,
                customerId: request.CustomerId,
                orderType: request.OrderType,
                subtotal: request.Subtotal,
                taxAmount: request.TaxAmount,
                discountAmount: request.DiscountAmount,
                totalAmount: request.TotalAmount,
                notes: request.Notes,
                createdByUserId: request.CreatedByUserId
            );

            foreach (var item in request.Items)
            {
                var orderDetail = OrderDetail.Create(
                    orderId: order.Id,
                    itemId: item.ItemId,
                    itemVariantId: item.ItemVariantId,
                    quantity: item.Quantity,
                    unitPrice: item.UnitPrice,
                    notes: item.Notes
                );
                order.AddOrderDetail(orderDetail);
            }

            order.Id = await _salesRepository.AddOrderAsync(order);

            return order.Id;
        }
    }
}