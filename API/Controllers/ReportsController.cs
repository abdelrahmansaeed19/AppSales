using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly ReportService _service;

    public ReportsController(ReportService service)
    {
        _service = service;
    }

    [HttpGet("inventory")]
    public IActionResult InventoryReport()
    {
        long tenantId = 1;
        return Ok(_service.Inventory(tenantId));
    }

    [HttpGet("sales")]
    public IActionResult SalesReport(DateTime from, DateTime to)
    {
        long tenantId = 1;
        return Ok(_service.Sales(tenantId, from, to));
    }
}
