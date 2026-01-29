using Infrastructure.Persistence.Contexts;
using Domain.Entities.Journal;
using Domain.Entities.Accounts;
using Application.Interfaces.IRepository;

public class AccountingRepository : IAccountingRepository
{
    private readonly AppSalesDbContext  _context;

    public AccountingRepository(AppSalesDbContext context)
    {
        _context = context;
    }

    public void AddJournalEntry(JournalEntry entry)
    {
        _context.JournalEntries.Add(entry);
    }

    public List<JournalLine> GetJournalLines(long tenantId)
    {
        return _context.JournalEntries
            .Where(j => j.TenantId == tenantId)
            .SelectMany(j => j.Lines)
            .ToList();
    }

    public List<Account> GetAccounts()
        => _context.Accounts.ToList();

    public void Save()
        => _context.SaveChanges();
}
