using MediatR;
using Domain.Entities.Users; // Change this to the correct namespace where the User class is defined
using Domain.Enums;
using Application.Interfaces.IRepository;

namespace Application.Modules.Users.Commands
{
    public record CreateUserCommand(
        long tenantId,
        long branchId,
        string Username,
        string Email,
        string Password,
        UserRole Role
        ) : IRequest<long>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                throw new InvalidOperationException($"A user with the email {request.Email} already exists.");
            }

            var user = User.Create(
                tenantId: request.tenantId,
                name: request.Username,
                email: request.Email,
                passwordHash: HashPassword(request.Password),
                role: request.Role,
                branchId: request.branchId
            );


            await _userRepository.AddAsync(user);

            return user.Id;
        }
        private string HashPassword(string password)
        {
            // Implement password hashing logic here
            return BCrypt.Net.BCrypt.HashPassword(password); // Placeholder
        }
    }
}