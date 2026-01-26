using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
    }
}
