using Application.Interfaces.IRepository;
using Domain.Entities.Expenses;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppSalesDbContext _context;

        public ExpenseRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetAllAsync(int tenantId, int? branchId = null)
        {
            var query = _context.Expenses.AsQueryable()
                                         .Where(e => e.TenantId == tenantId);

            if (branchId.HasValue)
                query = query.Where(e => e.BranchId == branchId.Value);

            return await query.ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(long id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
    }
}
