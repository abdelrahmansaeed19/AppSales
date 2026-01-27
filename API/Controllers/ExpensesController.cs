using API.Contracts_DTOs_.Expenses;
using Application.Interfaces.IServices;
using Domain.Entities.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int tenantId, [FromQuery] int? branchId)
        {
            var expenses = await _expenseService.GetAllExpenses(tenantId, branchId);
            return Ok(expenses.Select(e => new ExpenseResponse
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                Date = e.Date,
                Category = e.Category,
                AttachmentUrl = e.AttachmentUrl,
                TenantId = e.TenantId,
                BranchId = e.BranchId
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var expense = await _expenseService.GetExpenseById(id);
            if (expense == null) return NotFound();
            return Ok(new ExpenseResponse
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                Category = expense.Category,
                AttachmentUrl = expense.AttachmentUrl,
                TenantId = expense.TenantId,
                BranchId = expense.BranchId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExpenseRequest request)
        {
            try
            {
                var expense = new Expense
                {
                    Description = request.Description,
                    Amount = request.Amount,
                    Date = request.Date,
                    Category = request.Category,
                    AttachmentUrl = request.AttachmentUrl,
                    TenantId = request.TenantId,
                    BranchId = request.BranchId
                };

                var result = await _expenseService.CreateExpense(expense);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = new
                    {
                        code = "VALIDATION_ERROR",
                        message = ex.Message
                    }
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CreateExpenseRequest request)
        {
            var expense = await _expenseService.GetExpenseById(id);
            if (expense == null) return NotFound();

            expense.Description = request.Description;
            expense.Amount = request.Amount;
            expense.Date = request.Date;
            expense.Category = request.Category;
            expense.AttachmentUrl = request.AttachmentUrl;
            expense.BranchId = request.BranchId;

            await _expenseService.UpdateExpense(expense);

            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _expenseService.DeleteExpense(id);
            return NoContent();
        }
    }
}
