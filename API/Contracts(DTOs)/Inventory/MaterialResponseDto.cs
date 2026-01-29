using Microsoft.EntityFrameworkCore;

namespace App_Sales.DTO.InventoryDTO.MaterialsDTO
{
    public class MaterialResponseDto
    {
        public long Id {  get; set; }
         public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal CurrentQuantity { get; set; } = 0.00m;
        public decimal MinQuantity { get; set; } = 0.00m;
        public decimal? CostPerUnit { get; set; }
        public DateTime? ExpiryDate { get; set; }

    }
}
