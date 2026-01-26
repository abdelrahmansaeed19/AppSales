using Application.Interfaces.IRepository.Auth;
using Application.Interfaces.IServices.Auth;
using Application.Modules.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResult>;

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResult>
    {
        private readonly IRefreshTokenRepository _repo;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IRefreshTokenRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var token = await _repo.GetValidAsync(request.RefreshToken)
                ?? throw new InvalidOperationException("Invalid refresh token");

            var user = token.User;

            return new AuthResult
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = token.Token
            };
        }
    }

}
