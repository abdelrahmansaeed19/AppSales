using System;
using System.Collections.Generic;
using System.Text;

using Application.Interfaces.IRepository;
using Application.Modules.Users.DTOs;
using MediatR;

namespace Application.Modules.Users.Queries
{
    public record GetUsersByTenantQuery(long TenantId) : IRequest<List<UserDto>>;

    public class GetUsersByTenantQueryHandler : IRequestHandler<GetUsersByTenantQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersByTenantQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDto>> Handle(GetUsersByTenantQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllByTenantIdAsync(request.TenantId);

            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException($"No users found for TenantId {request.TenantId}");
            }

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                BranchId = user.BranchId
            }).ToList();

            return userDtos;
        }
    }
}
