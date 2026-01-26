using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Modules.Users.DTOs
{
    public record UserDto
    {
        public long Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Email { get; init; }
        public UserRole Role { get; init; }
        public bool IsActive { get; init; }
        public long? BranchId { get; init; }
    }
}
