using App_Sales.Data;
using App_Sales.Models;
using App_Sales.Models.Accounting;
using App_Sales.Repository.AccountingRepository;

public class AccountingRepository : IAccountingRepository
{
    private readonly App_Context _context;

    public AccountingRepository(App_Context context)
    {
        _context = context;
    }

    public void AddJournalEntry(JournalEntry entry)
    {
        _context.journalentry.Add(entry);
    }

    public List<JournalLine> GetJournalLines(long tenantId)
    {
        return _context.journalentry
            .Where(j => j.TenantId == tenantId)
            .SelectMany(j => j.Lines)
            .ToList();
    }

    public List<Account> GetAccounts()
        => _context.account.ToList();

    public void Save()
        => _context.SaveChanges();
}
