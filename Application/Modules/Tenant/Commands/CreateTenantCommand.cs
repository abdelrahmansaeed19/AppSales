using MediatR;
using Domain.Entities.Tenants;
using Application.Interfaces.IRepository;


namespace Application.Modules.Tenants.Commands
{
    public record CreateTenantCommand(
        string Name,
        string Email,
        string? Phone = null,
        string? Address = null,
        string? Logo = null,
        string Currency = "EGP",
        decimal TaxRate = 0.00m
    ) : IRequest<long>;

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, long>
    {
        private readonly ITenantRepository _tenantRepository;
        public CreateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<long> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            if(await _tenantRepository.ExistsByEmailAsync(request.Email))
            {
                throw new InvalidOperationException($"A tenant with the name '{request.Name}' already exists.");
            }

            var tenant = Domain.Entities.Tenants.Tenant.Create(
                request.Name,
                request.Email,
                request.Phone,
                request.Address,
                request.Logo,
                request.Currency,
                request.TaxRate
            );

            var tenantId = await _tenantRepository.AddAsync(tenant);

            return tenantId;
        }
    }
}