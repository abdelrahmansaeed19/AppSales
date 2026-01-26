using Domain.Entities.Users;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Auth
{
    public class EmailVerificationCode
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public long TenantId { get; set; }
        public long BranchId { get; set; }

        public string Code { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsUsed { get; set; } = false;
    }


}
