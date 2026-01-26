using Application.Interfaces.IRepository.Auth;
using Domain.Entities.Auth;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Auth
{
  public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppSalesDbContext _context;
        public RefreshTokenRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetValidAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == token && t.ExpiresAt > DateTime.UtcNow && !t.IsRevoked);
        }

        public async Task RevokeAsync(string token)
        {
            var existing = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (existing != null)
            {
                existing.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
