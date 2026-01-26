using Application.Interfaces.IServices;
using Application.Modules.Customers.Commands;
using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerService _service;

        public CreateCustomerHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<CustomerResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateCustomerAsync(new CreateCustomerRequest
            {
                Name = request.Name,
                InitialBalance = request.InitialBalance
            });
        }
    }
}
