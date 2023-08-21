using MediatR;
using Mladim.Domain.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjectStatistics;

public class GetProjectStatisticQuery : IRequest<ProjectStatisticsQueryDto>
{
    public int ProjectId { get; set; }
}
