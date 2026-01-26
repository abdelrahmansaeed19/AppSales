using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.DTO
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
    }
}
