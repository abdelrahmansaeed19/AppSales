namespace Domain.Entities.Journal
{
    public class JournalLine
    {
        public long Id { get; set; }
        public long JournalEntryId { get; set; }
        public long AccountId { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
