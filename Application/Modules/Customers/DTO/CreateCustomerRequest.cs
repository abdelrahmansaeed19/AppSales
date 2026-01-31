using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Customers.DTO
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal InitialBalance { get; set; } = 0;
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
