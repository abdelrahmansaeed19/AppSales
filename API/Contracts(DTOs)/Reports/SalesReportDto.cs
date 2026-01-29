namespace App_Sales.DTO.ReportDTO
{
    public class SalesReportDto
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
        public int OrdersCount { get; set; }
    }
}
