using Application.Modules.Sales.DTOs;
using Domain.Entities.Sales;
using Domain.Enums;

namespace Application.Interfaces.IRepository
{
    public interface ISaleRepository
    {
        Task<long> AddOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(long id);

        Task<OrderDto?> GetOrderDtoByIdAsync(long id);

        Task<List<OrderSummaryDto>> GetOrdersByBranchAsync(long branchId);

        Task<List<OrderSummaryDto>> GetOrdersByTenantAsync(long tenantId);

        Task<List<OrderSummaryDto>> GetOrdersByStatusAsync(long branchId, OrderStatus status);
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        // Get order status
        Task<OrderStatus?> GetOrderStatusByIdAsync(long id);

        // Get order paid amount
        Task<decimal?> GetOrderPaidAmountByIdAsync(long id);

        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(long id);
        Task<bool> OrderExistsAsync(long id);
        Task<long> AddOrderDetailAsync(OrderDetail orderDetail);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task DeleteOrderDetailAsync(long id);
    }
}