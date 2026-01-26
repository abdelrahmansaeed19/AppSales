using Application.Interfaces.IServices;
using Application.Modules.Customers.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerService _service;

        public DeleteCustomerCommandHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteCustomerAsync(request.Id);
            return Unit.Value;
        }
    }
}
