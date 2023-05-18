using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjectDetails;

public class GetProjectQuery : IRequest<ProjectDto>
{
    public int ProjectId { get; set; }
}
