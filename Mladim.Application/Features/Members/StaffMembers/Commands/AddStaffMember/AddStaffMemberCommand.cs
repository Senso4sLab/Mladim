using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;

public class AddStaffMemberCommand : IRequest<StaffMemberDetailsQueryDto>
{
    public int OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public int? YearOfBirth { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }
    public string Email { get; set; } = string.Empty;
    public ApplicationClaim Claim { get; set; } 
}
