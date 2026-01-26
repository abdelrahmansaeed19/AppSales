using Application.Modules.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices.Auth
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
    }
}
