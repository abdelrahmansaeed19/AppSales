using Application.Modules.Tenant.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Tenant.Queries
{
    public class GetAllTenantsQuery : IRequest<IEnumerable<TenantDto>>
    {
    }
}
