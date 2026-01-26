using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Auth;
using Application.Interfaces.IServices.Auth;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record ResendVerificationCommand(string Email) : IRequest;

    public class ResendVerificationCommandHandler : IRequestHandler<ResendVerificationCommand>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailVerificationRepository _codeRepo;
        private readonly IEmailService _emailService;

        public ResendVerificationCommandHandler(
            IUserRepository userRepo,
            IEmailVerificationRepository codeRepo,
            IEmailService emailService)
        {
            _userRepo = userRepo;
            _codeRepo = codeRepo;
            _emailService = emailService;
        }

        public async Task Handle(ResendVerificationCommand request, CancellationToken ct)
        {
            var temp = await _codeRepo.GetLatestByEmailAsync(request.Email)
                ?? throw new InvalidOperationException("No pending verification found");

            // Invalidate old codes
            await _codeRepo.InvalidateOldCodesByEmail(request.Email);

            // Create new code
            var newCode = new EmailVerificationCode
            {
                Name = temp.Name,
                Email = temp.Email,
                PasswordHash = temp.PasswordHash,
                Role = temp.Role,
                TenantId = temp.TenantId,
                Code = new Random().Next(100000, 999999).ToString(),
                ExpiryDate = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            await _codeRepo.AddAsync(newCode);
            await _emailService.SendVerificationEmailAsync(newCode.Email, newCode.Code);
        }

    }

}
