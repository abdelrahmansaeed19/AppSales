using Application.Modules.Auth.DTOs;
using MediatR;

namespace Application.Modules.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResult>;

}
