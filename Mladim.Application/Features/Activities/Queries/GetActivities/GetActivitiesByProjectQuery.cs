using MediatR;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesByQuery : IRequest<IEnumerable<ActivityQueryDto>>
{
    public int? ProjectId { get; set; }
    public int? OrganizationId { get; set;}
    public int? UpcomingActivities { get; set; }
  
}
