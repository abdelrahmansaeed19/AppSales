using Application.Interfaces.IRepository;
using Infrastructure.Persistence.Contexts;
using Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Application.Modules.Sales.DTOs;

namespace Infrastructure.Repositories
{
    public class SalesRepository : ISaleRepository
    {
        private readonly AppSalesDbContext _context;

        public SalesRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order?> GetOrderByIdAsync(long id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)  // impoetant to compare order with its details
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<OrderDto?> GetOrderDtoByIdAsync(long id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.OrderDetails)  // impoetant to compare order with its details
                .Where(o => o.Id == id)
                .Select(o => new OrderDto(
                    o.Id,
                    o.TenantId,
                    o.BranchId,
                    o.OrderType.ToString(),
                    o.Status.ToString(),
                    o.Subtotal,
                    o.TaxAmount,
                    o.DiscountAmount,
                    o.TotalAmount,
                    o.PaidAmount,
                    o.Notes,
                    o.CreatedAt,
                    o.OrderDetails.Select(od => new OrderDetailDto(
                        od.ItemId,
                        od.ItemVariantId,
                        od.Quantity,
                        od.UnitPrice,
                        od.TotalPrice,
                        od.Notes
                    )).ToList()
                    )).FirstOrDefaultAsync();
        }

        public async Task<List<OrderSummaryDto>> GetOrdersByBranchAsync(long branchId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.BranchId == branchId)
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
                )).ToListAsync();
        }

        public async Task<List<OrderSummaryDto>> GetOrdersByTenantAsync(long tenantId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.TenantId == tenantId)
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
                )).ToListAsync();
        }

        public async Task<List<OrderSummaryDto>> GetOrdersByStatusAsync(long branchId, OrderStatus status)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.BranchId == branchId && o.Status == status)
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
                )).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Get order status by id
        public async Task<OrderStatus?> GetOrderStatusByIdAsync(long id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => o.Status)
                .Cast<OrderStatus?>()
                .FirstOrDefaultAsync();
        }

        // Get paid amount by id
        public async Task<decimal?> GetOrderPaidAmountByIdAsync(long id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .Select(o => o.PaidAmount)
                .Cast<decimal?>()
                .FirstOrDefaultAsync();
        }

        public async Task DeleteOrderAsync(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> OrderExistsAsync(long id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId)
        {
            return await _context.OrderDetails
                                 .Where(od => od.OrderId == orderId)
                                 .ToListAsync();
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetailAsync(long id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
