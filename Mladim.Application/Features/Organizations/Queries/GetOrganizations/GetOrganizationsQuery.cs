using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsQuery : IRequest<IEnumerable<OrganizationDto>>
{
    public string AppUserId { get; set; }
}
