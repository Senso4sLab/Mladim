using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.ResetStaffMemberPassword;

public class ResetStaffMemberPasswordCommand : IRequest<bool>
{
    public string Email { get; set; } = default!;
}
