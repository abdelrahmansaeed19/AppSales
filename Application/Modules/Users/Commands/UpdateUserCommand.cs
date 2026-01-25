using MediatR;
using Domain.Enums;
using Application.Interfaces.IRepository;

namespace Application.Modules.Users.Commands
{

    // Change the record declaration to implement MediatR.IRequest<Unit>
    public record UpdateUserCommand(long UserId, string? Username, string? Email, string? Password, UserRole? Role) : IRequest<Unit>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {request.UserId} does not exist.");
            }
            if (!string.IsNullOrEmpty(request.Username))
            {
                user.Name = request.Username;
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                user.Email = request.Email;
            }
            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = HashPassword(request.Password);
            }
            if (request.Role.HasValue)
            {
                user.Role = request.Role.Value;
            }

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
        private string HashPassword(string password)
        {
            // Implement password hashing logic here
            return BCrypt.Net.BCrypt.HashPassword(password); // Placeholder
        }
    }
}
