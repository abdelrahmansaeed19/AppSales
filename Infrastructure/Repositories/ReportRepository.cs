using App_Sales.Repository.ReportRepository;

using Application.Modules.Inventory.DTOs;
using Infrastructure.Persistence.Contexts;

public class ReportsRepository : IReportsRepository
{
    private readonly AppSalesDbContext _context;

    public ReportsRepository(AppSalesDbContext context)
    {
        _context = context;
    }

    public List<InventoryReportDto> GetInventoryReport(long tenantId)
    {
        return _context.Items
            .Where(i => i.TenantId == tenantId)
            .Select(i => new InventoryReportDto
            {
                ItemId = i.Id,
                ItemName = i.Name,
                CurrentStock = i.CurrentStock,
                MinStockLevel = i.MinStockLevel,
                IsLowStock = i.CurrentStock <= i.MinStockLevel
            })
            .ToList();
    }

    //public List<SalesReportDto> GetSalesReport(long tenantId, DateTime from, DateTime to)
    //{
    //    return _context.Ord
    //        .Where(o => o.TenantId == tenantId && o.Date >= from && o.Date <= to)
    //        .GroupBy(o => o.Date.Date)
    //        .Select(g => new SalesReportDto
    //        {
    //            Date = g.Key,
    //            TotalSales = g.Sum(x => x.TotalAmount),
    //            OrdersCount = g.Count()
    //        })
    //        .ToList();
    //}
}
