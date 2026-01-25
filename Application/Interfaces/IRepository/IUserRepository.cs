using Domain.Entities.Users;
using MediatR;

namespace Application.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(string email);

        Task DeleteAsync(string email);

        Task<User?> GetByIdAsync(long id);

        // NEW: Explicit update (optional if you rely on EF Core change tracking, but good for pattern)
        Task UpdateAsync(User user);
    }
}