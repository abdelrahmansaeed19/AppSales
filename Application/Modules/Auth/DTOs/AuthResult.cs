using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Auth.DTOs
{
    public class AuthResult
    {
        public UserDto User { get; set; } = null!;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; } = 3600; // token lifetime in seconds
    }
}
