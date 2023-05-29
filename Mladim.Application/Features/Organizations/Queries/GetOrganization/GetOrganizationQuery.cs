using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganization;

public class GetOrganizationQuery : IRequest<OrganizationQueryDto>
{
    public int OrganizationId { get; set; }
}
