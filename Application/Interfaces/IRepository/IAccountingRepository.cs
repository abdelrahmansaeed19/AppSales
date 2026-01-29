using Domain.Entities.Journal;
using Domain.Entities.Accounts;

namespace Application.Interfaces.IRepository
{
    public interface IAccountingRepository
    {
        void AddJournalEntry(JournalEntry entry);
        List<JournalLine> GetJournalLines(long tenantId);
        List<Account> GetAccounts();
        void Save();
    }
}
