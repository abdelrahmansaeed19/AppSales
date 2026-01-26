using MediatR;
using Application.Modules.Tenant.DTOs;
using Application.Interfaces.IRepository;

namespace Application.Modules.Tenant.Queries
{

    public record GetTenantByIdQuery(long TenantId) : IRequest<TenantDto>;
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDto>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantByIdQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<TenantDto> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.TenantId);
            if (tenant == null)
            {
                throw new KeyNotFoundException($"Tenant with ID {request.TenantId} not found.");
            }

            var tenantDto = new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                CreatedAt = tenant.CreatedAt
            };

            return tenantDto;
        }
    }
}
