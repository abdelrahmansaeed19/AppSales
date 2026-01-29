using Application.Modules.Inventory.DTOs;

namespace App_Sales.Repository.ReportRepository
{
    public interface IReportsRepository
    {
        List<InventoryReportDto> GetInventoryReport(long tenantId);
        //List<SalesReportDto> GetSalesReport(long tenantId, DateTime from, DateTime to);
    }
}
