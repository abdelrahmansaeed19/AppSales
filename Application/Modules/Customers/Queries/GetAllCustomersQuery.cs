using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerResponse>>
    {
    }
}
