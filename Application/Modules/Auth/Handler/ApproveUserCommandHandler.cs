using Application.Interfaces.IRepository;
using Application.Modules.Auth.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Auth.Handler
{
    public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;

        public ApproveUserCommandHandler(IUserRepository userRepo)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public async Task<Unit> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {request.UserId} not found.");
            }

            user.IsActive = true;
            await _userRepo.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
