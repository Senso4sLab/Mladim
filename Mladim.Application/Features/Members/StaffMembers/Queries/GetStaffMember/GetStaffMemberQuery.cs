using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMember;

public class GetStaffMemberQuery : IRequest<StaffMemberDto>
{
    public int StaffMemberId { get; set; }
}
