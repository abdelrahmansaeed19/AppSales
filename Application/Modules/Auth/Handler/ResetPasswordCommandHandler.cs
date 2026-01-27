using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Auth;
using Application.Modules.Auth.Commands;
using Domain.Entities.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Auth.Handler
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationRepository _emailVerificationRepository;

        public ResetPasswordCommandHandler(
            IUserRepository userRepository,
            IEmailVerificationRepository emailVerificationRepository)
        {
            _userRepository = userRepository;
            _emailVerificationRepository = emailVerificationRepository;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken ct)
        {
            // 1️ Check the verification code
            var verification = await _emailVerificationRepository.GetValidCodeAsync(request.Email, request.Code);
            if (verification == null)
                throw new InvalidOperationException("Invalid or expired code.");

            // 2️ Get the user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            // 3️ Hash the new password with BCrypt
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            // 4️ Update the user
            await _userRepository.UpdateAsync(user);

            // 5️ Mark the verification code as used
            verification.IsUsed = true;
            await _emailVerificationRepository.UpdateAsync(verification);

            return Unit.Value; 
        }

    }
}
