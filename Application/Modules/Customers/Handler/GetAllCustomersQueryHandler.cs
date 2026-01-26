using Application.Interfaces.IServices;
using Application.Modules.Customers.DTO;
using Application.Modules.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerResponse>>
    {
        private readonly ICustomerService _service;

        public GetAllCustomersQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllCustomersAsync();
        }
    }
}
