namespace Application.Modules.Inventory.DTOs
{
    public class LedgerDto
    {
        public string AccountName { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
