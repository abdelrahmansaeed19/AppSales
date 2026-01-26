using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record RegisterCommand(
      string Name,
      string Email,
      string Password,
       UserRole Role,
      long TenantId,
      long BranchId

  ) : IRequest;
}
