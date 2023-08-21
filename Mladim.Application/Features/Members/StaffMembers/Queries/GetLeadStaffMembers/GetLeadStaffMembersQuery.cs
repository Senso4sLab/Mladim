using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetLeadStaffMembers;

public class GetLeadStaffMembersQuery : IRequest<IEnumerable<StaffMemberLeadQueryDto>>
{
    public int OrganizationId { get; set; }
}
