using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Transaction = Domain.Entities.Customers.Transaction;

namespace Application.Interfaces.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<List<Customer>> GetAllAsync();
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<List<Transaction>> GetCustomerTransactionsAsync(int customerId);
    }
}
