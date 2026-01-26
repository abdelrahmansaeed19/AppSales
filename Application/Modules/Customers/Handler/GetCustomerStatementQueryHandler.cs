using Application.Interfaces.IServices;
using Application.Modules.Customers.DTO;
using Application.Modules.Customers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Handler
{
    public class GetCustomerStatementQueryHandler : IRequestHandler<GetCustomerStatementQuery, CustomerStatementResponse>
    {
        private readonly ICustomerService _service;

        public GetCustomerStatementQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<CustomerStatementResponse> Handle(GetCustomerStatementQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCustomerStatementAsync(request.CustomerId);
        }
    }
}
