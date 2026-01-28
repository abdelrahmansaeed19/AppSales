using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record ForgotPasswordCommand(string Email) : IRequest<Unit>;

}
