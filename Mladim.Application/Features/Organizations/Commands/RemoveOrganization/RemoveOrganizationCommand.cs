using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.DeleteOrganization;

public class RemoveOrganizationCommand : IRequest<bool>
{
    public int OrganizationId { get; set; }
}
