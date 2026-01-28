using Application.Interfaces.IRepository;
using Application.Modules.Branches.DTOs;
using MediatR;

namespace Application.Modules.Branches.Queries
{
    public record GetBranchesByTenantQuery(long TenantId) : IRequest<List<BranchDto>>;
    public class GetBranchesByTenantQueryHandler : IRequestHandler<GetBranchesByTenantQuery, List<BranchDto>>
    {
        private readonly IBranchRepository _branchRepository;
        public GetBranchesByTenantQueryHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<List<BranchDto>> Handle(GetBranchesByTenantQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetByTenantIdAsync(request.TenantId);

            if (branches == null || !branches.Any())
            {
                throw new KeyNotFoundException($"No branches found for TenantId {request.TenantId}");
            }

            var branchDtos = branches.Select(branch => new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                Phone = branch.Phone,
                Email = branch.Email,
                IsActive = branch.IsActive,
                TenantId = branch.TenantId
            }).ToList();

            return branchDtos;
        }
    }
}
