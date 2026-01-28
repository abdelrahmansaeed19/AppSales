using Domain.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IExpenseService
    {
        Task<Expense?> GetExpenseById(long id);
        Task<IEnumerable<Expense>> GetAllExpenses(int tenantId, int? branchId = null);
        Task<Expense> CreateExpense(Expense expense);
        Task<Expense> UpdateExpense(Expense expense);
        Task DeleteExpense(long id);
    }
}
