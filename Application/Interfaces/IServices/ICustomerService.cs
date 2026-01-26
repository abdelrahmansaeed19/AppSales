using Application.Modules.Customers.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{

    public interface ICustomerService
    {
        Task<CustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);
        Task<CustomerResponse> UpdateCustomerAsync(int id, UpdateCustomerRequest request);
        Task DeleteCustomerAsync(int id);
        Task<CustomerResponse> GetCustomerByIdAsync(int id);
        Task<List<CustomerResponse>> GetAllCustomersAsync();
        Task<CustomerStatementResponse> GetCustomerStatementAsync(int customerId);
    }
}
