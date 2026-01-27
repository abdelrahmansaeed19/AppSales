namespace API.Contracts_DTOs_.Expenses
{
    public class CreateExpenseRequest
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Category { get; set; } = "General";
        public string? AttachmentUrl { get; set; }
        public int TenantId { get; set; }
        public int? BranchId { get; set; }
    }
}
