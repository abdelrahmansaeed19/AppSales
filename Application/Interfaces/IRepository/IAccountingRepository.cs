using App_Sales.Models.Accounting;

namespace App_Sales.Repository.AccountingRepository
{
    public interface IAccountingRepository
    {
        void AddJournalEntry(JournalEntry entry);
        List<JournalLine> GetJournalLines(long tenantId);
        List<Account> GetAccounts();
        void Save();
    }
}
