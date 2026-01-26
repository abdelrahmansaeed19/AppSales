using Application.Interfaces.IServices;
using Application.Modules.Customers.DTO;
using Application.Modules.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerService _service;

        public GetCustomerByIdQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCustomerByIdAsync(request.Id);
        }
    }
}
