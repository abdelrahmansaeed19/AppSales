using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Tenant.Commands
{
    public record DeleteTenantCommand(long Id) : IRequest<Unit>;

    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, Unit>
    {
        private readonly ITenantRepository _tenantRepository;

        public DeleteTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<Unit> Handle(DeleteTenantCommand command, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetByIdAsync(command.Id);

            if (tenant == null)
                throw new KeyNotFoundException($"Tenant with id {command.Id} not found.");

            await _tenantRepository.DeleteAsync(command.Id); // Actually remove it
            return Unit.Value;
        }
    }
}
