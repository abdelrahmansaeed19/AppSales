using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.DTO
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; } = 0;
    }
}
