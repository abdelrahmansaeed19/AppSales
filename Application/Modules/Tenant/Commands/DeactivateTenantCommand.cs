using Application.Interfaces.IRepository;
using Domain.Entities.Tenants;
using MediatR;



namespace Application.Modules.Tenants.Commands
{
    public record DeactivateTenantCommand(long Id) : IRequest<Unit>;
    public class DeactivateTenantCommandHandler : IRequestHandler<DeactivateTenantCommand, Unit>
    {
        private readonly ITenantRepository _tenantRepository;

        public DeactivateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Unit> Handle(DeactivateTenantCommand command, CancellationToken cancellationToken) {

            var tenant = await _tenantRepository.GetByIdAsync(command.Id);

            if (tenant == null)
            {
                throw new Exception("Tenant not found");
            }
            
            await _tenantRepository.DeleteAsync(command.Id);

            await _tenantRepository.UpdateAsync(tenant);

            return Unit.Value;
        }

    }
}
