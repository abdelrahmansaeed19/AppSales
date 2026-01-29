namespace Application.Modules.Inventory.DTOs
{
    public class IncomeStatementDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalRevenue - TotalExpenses;
    }
}
