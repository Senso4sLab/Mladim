using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizationStatistics;

public class GetOrganizationStatisticQuery : IRequest<OrganizationStatisticQueryDto>
{
    public int OrganizationId { get; set; }
    public int Year { get; set; }   
}
