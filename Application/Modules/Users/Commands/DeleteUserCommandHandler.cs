using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Users.Commands
{
    public record DeleteUserCommand(long UserId) : IRequest<Unit>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            // Get user by Id
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {command.UserId} does not exist.");
            }

            // Delete the user
            await _userRepository.DeleteAsync(user.Email);

            return Unit.Value;
        }
    
}
}
