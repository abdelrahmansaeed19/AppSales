using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Auth;
using Application.Interfaces.IServices.Auth;
using Application.Modules.Auth.Commands;
using Domain.Entities.Auth;
using Domain.Entities.Users;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Application.Modules.Auth.Handler
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailVerificationRepository _codeRepo;
        private readonly IEmailService _emailService;
        private readonly ITenantRepository _tenantRepo; 

        public RegisterCommandHandler(
            IUserRepository userRepo,
            IEmailVerificationRepository codeRepo,
            IEmailService emailService,
            ITenantRepository tenantRepo) 
        {
            _userRepo = userRepo;
            _codeRepo = codeRepo;
            _emailService = emailService;
            _tenantRepo = tenantRepo;
        }

        public async Task Handle(RegisterCommand request, CancellationToken ct)
        {
            // Check if email already exists in Users table
            if (await _userRepo.ExistsAsync(request.Email))
                throw new InvalidOperationException("Email already registered");

            // Invalidate old codes for this email
            await _codeRepo.InvalidateOldCodesByEmail(request.Email);

            // Create verification record (store user info temporarily)
            var code = new EmailVerificationCode
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                TenantId = request.TenantId,
                BranchId = request.BranchId,
                Code = new Random().Next(100000, 999999).ToString(),
                ExpiryDate = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            await _codeRepo.AddAsync(code);

            // Send verification email
            await _emailService.SendVerificationEmailAsync(code.Email, code.Code);
        }

    }
}
