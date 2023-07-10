using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQuery : IRequest<IEnumerable<MemberDto>>
{    
    public int? ProjectId { get; set; }
    public int? ActivityId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool WithDetails { get; set; } = false;
}
