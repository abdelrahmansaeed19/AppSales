using Application.Interfaces.IRepository;
using Application.Modules.Tenant.DTOs;
using Application.Modules.Tenant.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Tenant.Queries
{
    public class GetAllTenantsQueryHandler : IRequestHandler<GetAllTenantsQuery, IEnumerable<TenantDto>>
    {
        private readonly ITenantRepository _tenantRepository;

        public GetAllTenantsQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<IEnumerable<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.GetAllAsync();

            // Map Tenant entities to TenantDto
            var tenantDtos = tenants.Select(t => new TenantDto
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Address = t.Address
                // Map other fields as necessary
                // Add other fields if needed
            });

            return tenantDtos;
        }
    }
}