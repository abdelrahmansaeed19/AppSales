using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Entities.Tenants
{
    public class Branch
    {
        public long Id { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Tenant Tenant { get; set; } = null!;

        public static Branch Create(long tenantId, string name, string? address = null, string? phone = null, string? email = null)
        {
            return new Branch
            {
                TenantId = tenantId,
                Name = name,
                Address = address,
                Phone = phone,
                Email = email
            };
        }

        public void Update(string name, string? address = null, string? phone = null, string? email = null, bool isActive = true)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
