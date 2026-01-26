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

        public async Task<Tenant?> GetByIdAsync(long id)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
