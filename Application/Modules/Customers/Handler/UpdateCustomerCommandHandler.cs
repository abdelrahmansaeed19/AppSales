using Application.Interfaces.IServices;
using Application.Modules.Customers.Commands;
using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerService _service;

        public UpdateCustomerCommandHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateCustomerAsync(request.Id, new UpdateCustomerRequest
            {
                Name = request.Name,
                CurrentBalance = request.CurrentBalance
            });
        }
    }
}
