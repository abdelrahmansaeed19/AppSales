using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities.Expenses;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly AppSalesDbContext _context;

        public ExpenseService(IExpenseRepository expenseRepository , AppSalesDbContext context )
        {
            _expenseRepository = expenseRepository;
            _context = context;
        }

        public async Task<Expense> CreateExpense(Expense expense)
        {
            // Check if Tenant exists
            var tenantExists = await _context.Tenants.AnyAsync(t => t.Id == expense.TenantId);
            if (!tenantExists)
                throw new ArgumentException($"Tenant with ID {expense.TenantId} does not exist.");

            // Check if Branch exists (only if branchId is provided)
            if (expense.BranchId.HasValue)
            {
                var branchExists = await _context.Branches.AnyAsync(b => b.Id == expense.BranchId.Value && b.TenantId == expense.TenantId);
                if (!branchExists)
                    throw new ArgumentException($"Branch with ID {expense.BranchId.Value} does not exist for this tenant.");
            }

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task DeleteExpense(long id)
        {
            await _expenseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Expense>> GetAllExpenses(int tenantId, int? branchId = null)
        {
            return await _expenseRepository.GetAllAsync(tenantId, branchId);
        }

        public async Task<Expense?> GetExpenseById(long id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            await _expenseRepository.UpdateAsync(expense);
            return expense;
        }
    }
}
