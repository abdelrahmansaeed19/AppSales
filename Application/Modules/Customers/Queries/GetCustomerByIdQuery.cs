using Application.Modules.Customers.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    
}
}
