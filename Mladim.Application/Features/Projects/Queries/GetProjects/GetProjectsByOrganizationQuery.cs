using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsByOrganizationQuery : IRequest<IEnumerable<ProjectQueryDto>>
{
    public int OrganizationId { get; set; }
    public string UserId { get; set; }  = string.Empty;

    
}
