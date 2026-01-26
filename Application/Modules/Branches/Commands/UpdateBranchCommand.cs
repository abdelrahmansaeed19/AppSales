using MediatR;
using Application.Interfaces.IRepository;


namespace Application.Modules.Branches.Commands
{
    public record UpdateBranchCommand(
        long BranchId,
        string Name,
        string? Address,
        string? Phone,
        string? Email
    ) : IRequest<Unit>;
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Unit>
    {
        private readonly IBranchRepository _branchRepository;
        public UpdateBranchCommandHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.BranchId);

            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch with ID {request.BranchId} not found.");
            }

            branch.Update(
                request.Name,
                request.Address,
                request.Phone,
                request.Email
            );

            await _branchRepository.UpdateAsync(branch);

            return Unit.Value;
        }
    }
}
