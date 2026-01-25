using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Modules.Users.DTOs
{
    public record UserDto
    {
        long Id { get; init; }

        [Required]
        string Name { get; init; }

        [Required]
        string Email { get; init; }
        UserRole Role { get; init; }
        bool IsActive { get; init; }
        int? BranchId { get; init; }
    }
}
