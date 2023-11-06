using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Activities.Queries.GetActivity;
using Mladim.Domain.Dtos;

namespace Mladim.Application.Features.Activities.Queries.GetActivityName;

public class GetActivityNameQueryHandler : IRequestHandler<GetActivityNameQuery, string>
{
    public IUnitOfWork UnitOfWork { get; }
  
    public GetActivityNameQueryHandler(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        
    }
    public async Task<string> Handle(GetActivityNameQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
               .FindAsync(request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        return activity.Attributes.Name;

    }
}
