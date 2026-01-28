using Application.Interfaces.IRepository;
using Domain.Entities.Tenants;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppSalesDbContext _context;

        public BranchRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
            return branch.Id;
        }

        public async Task<Branch?> GetByIdAsync(long id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetByTenantIdAsync(long tenantId)
        {
            return await _context.Branches
                .AsNoTracking()
                .Where(b => b.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id) {
            Branch? branch = await GetByIdAsync(id);
            if (branch != null)
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Set<Branch>().AnyAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Set<Branch>().AnyAsync(b => b.Name == name);
        }
    }
}
