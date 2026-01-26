using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Application.Modules.Users.DTOs;


namespace Application.Modules.Users.Queries
{
    public record GetUserByIdQuery(long Id) : IRequest<UserDto>;
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.Id} not found.");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                BranchId = user.BranchId
            };

            return userDto;
        }
    }
}
