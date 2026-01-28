using MediatR;
using Application.Interfaces.IRepository;
using Domain.Entities.Tenants;

namespace Application.Modules.Branches.Commands
{
    public record CreateBranchCommand(
        long TenantId,
        string Name,
        string ? Address,
        string ? Phone,
        string ? Email
        ) : IRequest<long>;

    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, long>
    {
        private readonly IBranchRepository _branchRepository;

        public CreateBranchCommandHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<long> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var existingBranch = await _branchRepository.ExistsByNameAsync(request.Name);

            if (existingBranch)
            {
                throw new InvalidOperationException($"Branch with Name {request.Name} already exists.");
            }

            var branch = Branch.Create(
                request.TenantId,
                request.Name,
                request.Address,
                request.Phone,
                request.Email
            );

            await _branchRepository.AddAsync(branch);

            return branch.Id;
        }
    }
}
