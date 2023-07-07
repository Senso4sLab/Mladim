using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsQuery : IRequest<IEnumerable<OrganizationQueryDto>>
{
    public string AppUserId { get; set; } = string.Empty;
}
