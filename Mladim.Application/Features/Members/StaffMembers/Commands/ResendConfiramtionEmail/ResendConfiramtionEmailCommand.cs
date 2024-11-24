using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.ResetStaffMemberPassword;

public class ResendConfiramtionEmailCommand : IRequest<Result>
{
    public int OrganizationId { get; set; }
    public string Email { get; set; } = default!;
}
