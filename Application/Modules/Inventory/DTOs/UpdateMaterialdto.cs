using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Modules.Inventory.DTOs
{
    public class UpdateMaterialdto
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal CurrentQuantity { get; set; } = 0.00m;
        public decimal MinQuantity { get; set; } = 0.00m;
        public decimal? CostPerUnit { get; set; }
        public DateTime? ExpiryDate { get; set; }

    }
}

