using Domain.Entities.Tenants;

namespace Application.Interfaces.IRepository
{
    public interface IBranchRepository
    {
        Task<long> AddAsync(Branch branch);
        Task<Branch?> GetByIdAsync(long id);
        Task<IEnumerable<Branch>> GetAllAsync();

        // get Branches by TenantId
        Task<IEnumerable<Branch>> GetByTenantIdAsync(long tenantId);
        Task UpdateAsync(Branch branch);
        Task DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}
