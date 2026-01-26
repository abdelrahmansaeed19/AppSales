using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Tenant.DTOs
{
    public class TenantDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty ;

        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public decimal TaxRate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
