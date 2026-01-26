using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Commands
{
    public class CreateCustomerCommand : CreateCustomerRequest, IRequest<CustomerResponse>
    {
    }
}
