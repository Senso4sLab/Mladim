using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivity;

public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetActivityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
   

    public async Task<ActivityDto> Handle(GetActivityQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
            .GetByIdAsync(request.ActivityId, p => p.ActivityMembers, p => p.Partners, p => p.AnonymousParticipantGroups);

        if (activity == null)
            throw new Exception();

        return this.Mapper.Map<ActivityDto>(activity);
    }
}
