using Application.Interfaces.IRepository;
using Application.Modules.Branches.DTOs;
using Application.Modules.Branches.Queries;
using Domain.Entities.Tenants;
using MediatR;

namespace Application.Modules.Branches.Handlers
{
    public class GetBranchByIdHandler : IRequestHandler<GetBranchByIdQuery, BranchDto?>
    {
        private readonly IBranchRepository _branchRepository;

        public GetBranchByIdHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<BranchDto?> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id);
            if (branch == null) return null;

            return new BranchDto
            {
                Id = branch.Id,
                TenantId = branch.TenantId,
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone,
                Email = branch.Email,
                IsActive = branch.IsActive,
               
            };
        }
    }
}
