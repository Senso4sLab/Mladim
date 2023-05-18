using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsByOrganizationQuery : IRequest<IEnumerable<ProjectDto>>
{
    public int OrganizationId { get; set; }
}
