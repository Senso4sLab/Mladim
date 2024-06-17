using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizationsDescription;

public class GetOrganizationsDescriptionQuery : IRequest<IEnumerable<OrganizationAttributesShortQueryDto>>
{
    public int NumberOfOrganizations { get; set; }
}
