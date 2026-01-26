using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Auth
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        public User User { get; set; } = null!;
    }
}
