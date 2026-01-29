using App_Sales.Data;
using App_Sales.DTO.ReportDTO;
using App_Sales.Models;
using App_Sales.Repository.ReportRepository;

public class ReportsRepository : IReportsRepository
{
    private readonly App_Context _context;

    public ReportsRepository(App_Context context)
    {
        _context = context;
    }

    public List<InventoryReportDto> GetInventoryReport(long tenantId)
    {
        return _context.item
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

    public List<SalesReportDto> GetSalesReport(long tenantId, DateTime from, DateTime to)
    {
        return _context.order
            .Where(o => o.TenantId == tenantId && o.Date >= from && o.Date <= to)
            .GroupBy(o => o.Date.Date)
            .Select(g => new SalesReportDto
            {
                Date = g.Key,
                TotalSales = g.Sum(x => x.TotalAmount),
                OrdersCount = g.Count()
            })
            .ToList();
    }
}
