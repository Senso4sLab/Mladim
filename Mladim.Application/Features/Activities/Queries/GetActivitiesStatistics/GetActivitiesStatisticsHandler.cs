using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivitiesStatistics;

public class GetActivitiesStatisticsHandler : IRequestHandler<GetActivitiesStatisticsQuery, IEnumerable<ActivityStatisticQueryDto>>
{
    public Task<IEnumerable<ActivityStatisticQueryDto>> Handle(GetActivitiesStatisticsQuery request, CancellationToken cancellationToken)
    {
        //if (request.Start is DateTime start && request.End is DateTime end)
        //{
        //    var rangeDateTime = DateTimeRange.Create(start, end);

        //    var activities = await this.UnitOfWork.ActivityRepository
        //        .GetActivitiesWithProjectName(a => a.Project.OrganizationId == orgId && a.TimeRange.StartDate >= start && a.TimeRange.EndDate <= end, null);

        //    return this.Mapper.Map<IEnumerable<ActivityWithProjectNameQueryDto>>(activities);
        //}

        return;
    }
}
