using App_Sales.DTO.AccountingDTO;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/accounting")]
public class AccountingController : ControllerBase
{
    private readonly AccountingService _service;

    public AccountingController(AccountingService service)
    {
        _service = service;
    }

    [HttpPost("journal")]
    public IActionResult CreateJournal(CreateJournalEntryDto dto)
    {
        long tenantId = 1;
        _service.CreateJournalEntry(dto, tenantId);
        return Ok("Journal Entry Created");
    }

    [HttpGet("ledger")]
    public IActionResult GeneralLedger()
    {
        long tenantId = 1;
        return Ok(_service.GeneralLedger(tenantId));
    }

    [HttpGet("trial-balance")]
    public IActionResult TrialBalance()
    {
        long tenantId = 1;
        return Ok(_service.TrialBalance(tenantId));
    }
}
