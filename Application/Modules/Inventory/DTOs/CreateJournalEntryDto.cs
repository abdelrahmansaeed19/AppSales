namespace Application.Modules.Inventory.DTOs
{
    public class CreateJournalEntryDto
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public List<JournalLineDto> Lines { get; set; }
    }
}
