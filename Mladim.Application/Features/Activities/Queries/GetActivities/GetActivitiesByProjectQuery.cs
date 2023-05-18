using MediatR;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesByProjectQuery : IRequest<IEnumerable<ActivityDto>>
{
    public int ProjectId { get; set; }
}
