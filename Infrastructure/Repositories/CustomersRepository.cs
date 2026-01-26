using Application.Interfaces.IRepository;
using Domain.Entities.Customers;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Transaction = Domain.Entities.Customers.Transaction;


namespace Infrastructure.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppSalesDbContext _context;
        public CustomerRepository(AppSalesDbContext context) => _context = context;

        public async Task<Customer> GetByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Transactions)
                .FirstOrDefaultAsync(c => c.Id == id);

            return customer ?? throw new KeyNotFoundException($"Customer {id} not found");
        }
        public async Task<List<Customer>> GetAllAsync() =>
            await _context.Customers.ToListAsync();

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Transaction>> GetCustomerTransactionsAsync(int customerId) =>
            await _context.Transactions
                .Where(t => t.CustomerId == customerId)
                .ToListAsync();
    }
}