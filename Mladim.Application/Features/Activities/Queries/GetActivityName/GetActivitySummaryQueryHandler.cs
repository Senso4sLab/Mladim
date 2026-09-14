using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Activities.Queries.GetActivity;
using Mladim.Domain.Dtos;

namespace Mladim.Application.Features.Activities.Queries.GetActivityName;

public class GetActivitySummaryQueryHandler : IRequestHandler<GetActivitySummaryQuery, ActivitySummaryDto>
{
    public IUnitOfWork UnitOfWork { get; }
  
    public GetActivitySummaryQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        
    }
    public async Task<ActivitySummaryDto> Handle(GetActivitySummaryQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
               .FindAsync(request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        return new ActivitySummaryDto(activity.Attributes.Name, activity.Attributes.ActivityTargetGroup);

    }
}
