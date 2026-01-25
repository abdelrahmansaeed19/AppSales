namespace Domain.Entities.Expenses
{
    public class Expense
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = "General";
        public string? AttachmentUrl { get; set; }
        public int TenantId { get; set; }
        public int? BranchId { get; set; }
    }
}