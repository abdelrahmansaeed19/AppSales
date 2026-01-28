using MediatR;
using Application.Interfaces.IRepository;
using Domain.Entities.Sales;
using Domain.Enums;

namespace Application.Modules.Sales.Commands
{
    public record CreateOrderCommand(
        long tenantId, 
        long branchId,
        long? shiftId,
        long? customerId,
        OrderType orderType,
        OrderStatus status,
        decimal subtotal,
        decimal taxAmount,
        decimal discountAmount,
        decimal totalAmount,
        decimal paidAmount,
        string? notes,
        long? createdByUserId,
        ICollection<OrderDetail> orderDetails
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
            if (request.totalAmount < 0)
            {
                throw new ArgumentException("Total amount cannot be negative.");
            }

            var order = new Order
            {
                TenantId = request.tenantId,
                BranchId = request.branchId,
                ShiftId = request.shiftId,
                CustomerId = request.customerId,
                OrderType = request.orderType,
                Status = request.status,
                Subtotal = request.subtotal,
                TaxAmount = request.taxAmount,
                DiscountAmount = request.discountAmount,
                TotalAmount = request.totalAmount,
                PaidAmount = request.paidAmount,
                Notes = request.notes,
                CreatedByUserId = request.createdByUserId,
                OrderDetails = request.orderDetails
            };
            await _salesRepository.AddOrderAsync(order);

            return order.Id;
        }
    }
}