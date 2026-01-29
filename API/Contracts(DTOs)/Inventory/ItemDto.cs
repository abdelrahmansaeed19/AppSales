using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App_Sales.DTO.InventoryDTO.ItemDTO
{
    public class ItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public decimal SellingPrice { get; set; }
        public decimal CurrentStock { get; set; } = 0.00m;
        public decimal MinStockLevel { get; set; } = 0.00m;
        public bool IsActive { get; set; }

    }
}
