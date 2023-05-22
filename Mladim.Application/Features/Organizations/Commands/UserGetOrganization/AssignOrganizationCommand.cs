using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.UserGetOrganization;

public class AssignOrganizationCommand : IRequest<bool>
{
    public string AppUserId { get; set; }
    public int? OrganizationId { get; set; }
}
