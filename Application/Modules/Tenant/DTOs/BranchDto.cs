using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Tenant.DTOs
{
    public class BranchDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public long TenantId { get; set; }
    }
}
