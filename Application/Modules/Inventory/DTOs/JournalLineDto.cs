namespace Application.Modules.Inventory.DTOs
{
    public class JournalLineDto
    {
        public long AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
