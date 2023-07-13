using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Groups.Queries.GetGroups;

public class GetGroupsQuery : IRequest<IEnumerable<GroupQueryDto>>
{
    public int OrganizationId { get; set; }
    public bool IsActive { get; set; } = true;
}
