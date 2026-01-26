using Application.Interfaces.IRepository;
using Domain.Entities.Tenants;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppSalesDbContext _context;

        public TenantRepository(AppSalesDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddAsync(Tenant tenant)
        {
            await _context.Set<Tenant>().AddAsync(tenant);
            await _context.SaveChangesAsync();
            return tenant.Id;
        }


        public async Task<Tenant> GetByIdAsync(long id)
        {
            return await _context.Set<Tenant>().FindAsync(id);
        }
        public async Task DeleteAsync(long id)
        {
            Tenant? tenant = await GetByIdAsync(id);

            if (tenant != null)
            {
                _context.Set<Tenant>().Remove(tenant);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Branch>> GetBranchesByTenantIdAsync(long tenantId)
        {
            return await _context.Set<Branch>()
                .AsNoTracking()
                .Where(b => b.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            return await _context.Set<Tenant>().ToListAsync();
        }

        public async Task UpdateAsync(Tenant tenant)
        {
            _context.Set<Tenant>().Update(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Set<Tenant>().AnyAsync(t => t.Email == email);
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Set<Tenant>().AnyAsync(t => t.Id == id);
        }

    }
}
