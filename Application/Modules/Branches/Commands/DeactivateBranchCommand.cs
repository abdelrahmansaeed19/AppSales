using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Application.Interfaces.IRepository;

namespace Application.Modules.Branches.Commands
{
    public record DeactivateBranchCommand(long BranchId) : IRequest<Unit>;
    public class DeactivateBranchCommandHandler : IRequestHandler<DeactivateBranchCommand, Unit>
    {
        private readonly IBranchRepository _branchRepository;
        public DeactivateBranchCommandHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<Unit> Handle(DeactivateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.BranchId);

            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch with ID {request.BranchId} not found.");
            }

            branch.Deactivate();

            await _branchRepository.UpdateAsync(branch);

            return Unit.Value;
        }
    }
}
