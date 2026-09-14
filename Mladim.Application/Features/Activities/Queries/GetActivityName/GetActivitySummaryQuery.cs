using MediatR;
using Mladim.Domain.Dtos;

namespace Mladim.Application.Features.Activities.Queries.GetActivityName;

public class GetActivitySummaryQuery : IRequest<ActivitySummaryDto>
{
    public int ActivityId { get; set; }
}
