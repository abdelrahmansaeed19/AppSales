using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record VerifyEmailCommand(string Email, string Code) : IRequest;

}
