using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Commands
{
    public class UpdateCustomerCommand : UpdateCustomerRequest, IRequest<CustomerResponse>
    {
        public int Id { get; set; }  // Important: ID to know which customer to update
    }

}
