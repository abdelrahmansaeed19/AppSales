using Application.Modules.Inventory.DTOs;
using App_Sales.Repository.ReportRepository;

public class ReportService
{
    private readonly IReportsRepository _repo;

    public ReportService(IReportsRepository repo)
    {
        _repo = repo;
    }

    public List<InventoryReportDto> Inventory(long tenantId)
        => _repo.GetInventoryReport(tenantId);

    //public List<SalesReportDto> Sales(long tenantId, DateTime from, DateTime to)
    //    => _repo.GetSalesReport(tenantId, from, to);
}
