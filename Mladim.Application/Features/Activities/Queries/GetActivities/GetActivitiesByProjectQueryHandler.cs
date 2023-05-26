using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivities;

public class GetActivitiesByProjectQueryHandler : IRequestHandler<GetActivitiesByProjectQuery, IEnumerable<ActivityDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetActivitiesByProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
 

    public async Task<IEnumerable<ActivityDto>> Handle(GetActivitiesByProjectQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Activity, bool>> predicate = a => a.ProjectId == request.ProjectId;

        var activities = await this.UnitOfWork.ActivityRepository
            .GetAllAsync(new[] { predicate });

        return this.Mapper.Map<IEnumerable<ActivityDto>>(activities);   
    }
}
