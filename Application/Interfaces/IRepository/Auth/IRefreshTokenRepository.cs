using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository.Auth
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetValidAsync(string token); // Gets token if not expired and not revoked
        Task RevokeAsync(string token); // Optional: revoke a token
    }
}
