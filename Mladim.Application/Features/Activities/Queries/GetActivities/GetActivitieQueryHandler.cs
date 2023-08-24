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

public class GetActivitieQueryHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<ActivityQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public GetActivitieQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    } 

    public async Task<IEnumerable<ActivityQueryDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId is int projectId)
        {
            var activities = await this.UnitOfWork.ActivityRepository
                .GetActivitiesWithProjectName(a => a.ProjectId ==  projectId, request.UpcomingActivities);

            return this.Mapper.Map<IEnumerable<ActivityQueryDto>>(activities);
        }
        
        if(request.OrganizationId is int organizationId)
        {
            var activities = await this.UnitOfWork.ActivityRepository
                .GetActivitiesWithProjectName(a => a.Project.OrganizationId == organizationId, request.UpcomingActivities);         

            return this.Mapper.Map<IEnumerable<ActivityWithProjectNameQueryDto>>(activities);
        }
        
        return Enumerable.Empty<ActivityQueryDto>();
    }
}
