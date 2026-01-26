using Domain.Entities.Auth;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices.Auth
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(long userId);
    }
}
