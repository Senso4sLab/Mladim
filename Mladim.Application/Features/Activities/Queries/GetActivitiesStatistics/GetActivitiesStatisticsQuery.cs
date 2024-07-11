using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivitiesStatistics;

public class GetActivitiesStatisticsQuery : IRequest<IEnumerable<ActivityStatisticQueryDto>>
{
    public int OrganizationId { get; set; }
    public DateTimeRange DateTimeRange { get; set; }

}
