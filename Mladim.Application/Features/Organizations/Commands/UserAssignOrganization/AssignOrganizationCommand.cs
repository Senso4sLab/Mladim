using MediatR;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.UserGetOrganization;

public class AssignOrganizationCommand : IRequest<bool>
{
    public string AppUserId { get; set; } = string.Empty;
    public int OrganizationId { get; set; }
    public ApplicationClaim Claim { get; set;}
}
