using Domain.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IExpenseRepository
    {
        Task<Expense?> GetByIdAsync(long id);
        Task<IEnumerable<Expense>> GetAllAsync(int tenantId, int? branchId = null);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(long id);
    }
}
