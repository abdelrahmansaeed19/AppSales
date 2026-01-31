using Application.Modules.Branches.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Modules.Branches.Queries
{
    public record GetBranchByIdQuery(long Id) : IRequest<BranchDto?>;

}
