using Application.Interfaces.IRepository;
using Domain.Entities.Users;
using Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSalesDbContext _context;
        public UserRepository(AppSalesDbContext context) => _context = context;

        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Set<User>().Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task DeleteAsync(string email) {
            var user = await GetByEmailAsync(email);
            if (user != null) {
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
