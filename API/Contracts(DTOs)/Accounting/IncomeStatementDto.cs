namespace App_Sales.DTO.AccountingDTO
{
    public class IncomeStatementDto
    {
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalRevenue - TotalExpenses;
    }
}
