using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record ApproveUserCommand(long UserId) : IRequest<Unit>;
}
