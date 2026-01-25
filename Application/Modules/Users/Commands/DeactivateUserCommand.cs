using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Application.Interfaces.IRepository;
using System.Threading.Tasks;


namespace Application.Modules.Users.Commands
{
    public record DeactivateUserCommand(long UserId) : IRequest<Unit>;
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeactivateUserCommand command, CancellationToken cancellationToken) {
            var user = _userRepository.GetByIdAsync(command.UserId).Result;

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {command.UserId} does not exist.");
            }

            user.Deactivate();

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
