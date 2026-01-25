using Domain.Entities.Tenants;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Users
{
    public class User
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public long? BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Hashed
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Tenant Tenant { get; set; } = null!;
        public Branch? Branch { get; set; }

        public static User Create(int tenantId, string name, string email, string passwordHash, UserRole role, int? branchId)
        {
            return new User
            {
                TenantId = tenantId,
                Name = name,
                Email = email,
                Password = passwordHash,
                Role = role,
                BranchId = branchId
            };
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, UserRole role, long? branchId)
        {
            Name = name;
            BranchId = branchId;
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
