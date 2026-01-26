using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Application.Modules.Customers.DTO;
using Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                CurrentBalance = request.InitialBalance
            };

            var result = await _customerRepository.AddAsync(customer);

            return new CustomerResponse
            {
                Id = result.Id,
                Name = result.Name,
                CurrentBalance = result.CurrentBalance
            };
        }

        public async Task<CustomerResponse> UpdateCustomerAsync(int id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            customer.Name = request.Name;
            customer.CurrentBalance = request.CurrentBalance;

            var updated = await _customerRepository.UpdateAsync(customer);

            return new CustomerResponse
            {
                Id = updated.Id,
                Name = updated.Name,
                CurrentBalance = updated.CurrentBalance
            };
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null) return null;

            return new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                CurrentBalance = customer.CurrentBalance
            };
        }

        public async Task<List<CustomerResponse>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                CurrentBalance = c.CurrentBalance
            }).ToList();
        }

        public async Task<CustomerStatementResponse> GetCustomerStatementAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null) return null;

            var transactions = await _customerRepository.GetCustomerTransactionsAsync(customerId);

            return new CustomerStatementResponse
            {
                Customer = new CustomerResponse
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    CurrentBalance = customer.CurrentBalance
                },
                Transactions = transactions.Select(t => new Application.Modules.Customers.DTO.TransactionDto
                {
                    Type = t.Type,
                    Description = t.Description,
                    Debit = t.Debit,
                    Credit = t.Credit,
                    Balance = t.Balance,
                    Date = t.Date
                }).ToList()
            };
        }
    }
}
