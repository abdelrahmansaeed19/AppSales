using Domain.Entities.Tenants;

namespace Application.Interfaces.IRepository
{
    public interface ITenantRepository
    {
        Task<long> AddAsync(Tenant tenant);

        Task<Tenant> GetByIdAsync(long id);

        Task<IEnumerable<Tenant>> GetAllAsync();

        Task UpdateAsync(Tenant tenant);

        Task<List<Branch>> GetBranchesByTenantIdAsync(long tenantId);

        Task DeleteAsync(long id);

        Task<bool> ExistsByEmailAsync(string email);

        Task<bool> ExistsAsync(long id);

    }
}