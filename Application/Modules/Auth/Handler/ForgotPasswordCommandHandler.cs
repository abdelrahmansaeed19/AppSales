using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Auth;
using Application.Interfaces.IServices.Auth;
using Application.Modules.Auth.Commands;
using Domain.Entities.Auth;
using Domain.Entities.Users;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Auth.Handler
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IEmailVerificationRepository _emailVerificationRepository;

        public ForgotPasswordCommandHandler(
            IEmailService emailService,
            IUserRepository userRepository,
            IEmailVerificationRepository emailVerificationRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
            _emailVerificationRepository = emailVerificationRepository;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new InvalidOperationException("Email not found.");

            var code = new Random().Next(100000, 999999).ToString();

            var verification = new EmailVerificationCode
            {
                Email = user.Email,
                Code = code,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15),
                IsUsed = false,
                Name = user.Name,
                PasswordHash = user.Password,
                Role = user.Role,
                TenantId = user.TenantId,
                BranchId = user.BranchId ?? 0,
            };

            await _emailVerificationRepository.AddAsync(verification);
            await _emailService.SendResetPasswordEmailAsync(user.Email, code);

            return Unit.Value;
        }
    }
}
