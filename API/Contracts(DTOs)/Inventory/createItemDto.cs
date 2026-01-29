using System.Numerics;


namespace App_Sales.DTO.InventoryDTO.ItemDTO
{
    public class CreateItemDto
    {
        public long CategoryId {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public string? Image { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CurrentStock { get; set; } = 0.00m;
        public decimal MinStockLevel { get; set; } = 0.00m;
  

    }
}
