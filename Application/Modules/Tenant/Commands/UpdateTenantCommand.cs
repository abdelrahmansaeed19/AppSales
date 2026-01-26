using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Modules.Tenants.Commands
{
    public record UpdateTenantCommand(
        long Id, 
        string Name,
        string Email,
        string? Phone,
        string? Address,
        string? Logo,
        string Currency,
        decimal TaxRate
       ) : IRequest<Unit>;

    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, Unit>
    {
        private readonly ITenantRepository _tenantRepository;

        public UpdateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Unit> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(request.Id);

            if (tenant == null)
            {
                throw new KeyNotFoundException($"Tenant with ID '{request.Id}' not found.");
            }

            tenant.Update(
                request.Name,
                request.Email,
                request.Phone,
                request.Address,
                request.Logo,
                request.Currency,
                request.TaxRate
            );

            await _tenantRepository.UpdateAsync(tenant);

            return Unit.Value;
        }
    }
}
