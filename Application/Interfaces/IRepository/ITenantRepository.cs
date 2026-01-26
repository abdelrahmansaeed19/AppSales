using Domain.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface ITenantRepository
    {
        Task<Tenant?> GetByIdAsync(long id);
    }
}
