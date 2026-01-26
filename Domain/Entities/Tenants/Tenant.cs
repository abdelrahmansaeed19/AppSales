using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Tenants
{
    public class Tenant
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Logo { get; set; }
        public string Currency { get; set; } = "EGP";
        public decimal TaxRate { get; set; } = 0.00m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();


        public static Tenant Create(string name, string email, string? phone = null, string? address = null, string? logo = null, string currency = "EGP", decimal taxRate = 0.00m)
        {
            return new Tenant
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address,
                Logo = logo,
                Currency = currency,
                TaxRate = taxRate
            };
        }

        public void Update(string name, string email, string? phone = null, string? address = null, string? logo = null, string currency = "EGP", decimal taxRate = 0.00m)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            Logo = logo;
            Currency = currency;
            TaxRate = taxRate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
