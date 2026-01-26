using Application.Interfaces.IRepository.Auth;
using Domain.Entities.Auth;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Auth
{
    public class EmailVerificationRepository : IEmailVerificationRepository
    {
        private readonly AppSalesDbContext _context;

        public EmailVerificationRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmailVerificationCode code)
        {
            await _context.EmailVerificationCodes.AddAsync(code);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailVerificationCode?> GetValidCodeAsync(string email, string code)
        {
            return await _context.EmailVerificationCodes
                .FirstOrDefaultAsync(x =>
                    x.Email == email &&
                    x.Code == code &&
                    !x.IsUsed &&
                    x.ExpiryDate > DateTime.UtcNow);
        }

        public Task InvalidateOldCodes(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task InvalidateOldCodesByEmail(string email)
        {
            var codes = await _context.EmailVerificationCodes
                .Where(c => c.Email == email && !c.IsUsed)
                .ToListAsync();

            codes.ForEach(c => c.IsUsed = true);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmailVerificationCode code)
        {
            _context.EmailVerificationCodes.Update(code);
            await _context.SaveChangesAsync();
        }

        public async Task<EmailVerificationCode?> GetLatestByEmailAsync(string email)
        {
            return await _context.EmailVerificationCodes
                .Where(c => c.Email == email && !c.IsUsed)
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();
        }
    }

}
