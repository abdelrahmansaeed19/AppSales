namespace App_Sales.DTO.ReportDTO
{
    public class InventoryReportDto
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal MinStockLevel { get; set; }
        public bool IsLowStock { get; set; }
    }
}
