using Application.Modules.Inventory.DTOs;

using Application.Interfaces.IRepository;

using Domain.Entities.Journal;

public class AccountingService
{
    private readonly IAccountingRepository _repo;

    public AccountingService(IAccountingRepository repo)
    {
        _repo = repo;
    }

    public void CreateJournalEntry(CreateJournalEntryDto dto, long tenantId)
    {
        if (dto.Lines.Sum(l => l.Debit) != dto.Lines.Sum(l => l.Credit))
            throw new Exception("Debit must equal Credit");

        var entry = new JournalEntry
        {
            Date = dto.Date,
            Reference = dto.Reference,
            TenantId = tenantId,
            Lines = dto.Lines.Select(l => new JournalLine
            {
                AccountId = l.AccountId,
                Debit = l.Debit,
                Credit = l.Credit
            }).ToList()
        };

        _repo.AddJournalEntry(entry);
        _repo.Save();
    }

    public List<LedgerDto> GeneralLedger(long tenantId)
    {
        return _repo.GetJournalLines(tenantId)
            .GroupBy(l => l.AccountId)
            .Select(g => new LedgerDto
            {
                AccountName = _repo.GetAccounts()
                    .FirstOrDefault(a => a.Id == g.Key)?.Name ?? "Unknown",

                TotalDebit = g.Sum(x => x.Debit),
                TotalCredit = g.Sum(x => x.Credit)
            })
            .ToList();
    }

    public List<TrialBalanceDto> TrialBalance(long tenantId)
    {
        return _repo.GetJournalLines(tenantId)
            .GroupBy(l => l.AccountId)
            .Select(g => new TrialBalanceDto
            {
                AccountName = _repo.GetAccounts()
                    .FirstOrDefault(a => a.Id == g.Key)?.Name ?? "Unknown",

                Debit = g.Sum(x => x.Debit),
                Credit = g.Sum(x => x.Credit)
            })
            .ToList();
    }
}
