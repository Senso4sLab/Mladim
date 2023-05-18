using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQuery : IRequest<IEnumerable<StaffMemberDto>>
{
    public int? OrganizationId { get; set; }
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
}
