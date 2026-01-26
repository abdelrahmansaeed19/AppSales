using Application.Modules.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResult>;

}
