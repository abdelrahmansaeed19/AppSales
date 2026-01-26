using Application.Interfaces.IRepository;
using Application.Interfaces.IRepository.Auth;
using Application.Modules.Auth.Commands;
using Domain.Entities.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Handler
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailVerificationRepository _codeRepo;
        private readonly ITenantRepository _tenantRepo;

        public VerifyEmailCommandHandler(IUserRepository userRepo, IEmailVerificationRepository codeRepo , ITenantRepository tenantRepo)
        {
            _userRepo = userRepo;
            _codeRepo = codeRepo;
            _tenantRepo = tenantRepo;
        }

        public async Task Handle(VerifyEmailCommand request, CancellationToken ct)
        {
            // Get temp verification record
            var temp = await _codeRepo.GetValidCodeAsync(request.Email, request.Code)
                       ?? throw new InvalidOperationException("Invalid or expired code");

            // Check if user already exists
            var existingUser = await _userRepo.GetByEmailAsync(temp.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User already exists");

            // Check tenant exists
            var tenant = await _tenantRepo.GetByIdAsync(temp.TenantId);
            if (tenant == null)
                throw new InvalidOperationException($"Tenant with ID {temp.TenantId} does not exist");
            // Create the real user
            var user = new User
            {
                Name = temp.Name,
                Email = temp.Email,
                Password = temp.PasswordHash,
                Role = temp.Role,
                TenantId = temp.TenantId,
                BranchId = temp.BranchId,
                IsActive = true,
                IsEmailVerified = true,
            };

            // Add user
            try
            {
                await _userRepo.AddAsync(user);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Database error: " + ex.InnerException?.Message);
            }

            // Mark code as used
            temp.IsUsed = true;
            await _codeRepo.UpdateAsync(temp);
        }


    }

}
