using Application.Interfaces.IRepository;
using Application.Interfaces.IServices.Auth;
using Application.Modules.Auth.Commands;
using Application.Modules.Auth.DTOs;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Application.Modules.Auth.Handler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResult>
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUserRepository userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken ct)
        {
            // Get user by email
            var user = await _userRepo.GetByEmailAsync(request.Email)
                ?? throw new InvalidOperationException("Invalid credentials");

            if (!user.IsEmailVerified)
                throw new InvalidOperationException("Email not verified");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new InvalidOperationException("Invalid credentials");

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            // Map user info
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                TenantId = user.TenantId,
                BranchId = user.BranchId
            };

            return new AuthResult
            {
                User = userDto,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = 3600
            };
        }
    }
}
