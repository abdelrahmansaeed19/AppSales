namespace App_Sales.Models.Accounting
{
    public class JournalEntry
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public long TenantId { get; set; }

        public List<JournalLine> Lines { get; set; }
    }
}
