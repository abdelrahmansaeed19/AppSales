namespace Application.Modules.Inventory.DTOs
{
    public class BalanceSheetDto
    {
        public decimal Assets { get; set; }
        public decimal Liabilities { get; set; }
        public decimal Equity { get; set; }
    }
}
