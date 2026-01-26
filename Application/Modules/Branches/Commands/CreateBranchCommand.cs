using MediatR;
using Application.Interfaces.IRepository;
using Domain.Entities.Tenants;

namespace Application.Modules.Branches.Commands
{
    public record CreateBranchCommand(
        long Id,
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
            var existingBranch = await _branchRepository.GetByIdAsync(request.Id);

            if (existingBranch != null)
            {
                throw new InvalidOperationException($"Branch with ID {request.Id} already exists.");
            }

            var branch = Branch.Create(
                request.Id,
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
