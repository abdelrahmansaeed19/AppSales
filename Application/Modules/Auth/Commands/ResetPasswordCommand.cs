using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? NewPassword { get; set; }
    }
}
