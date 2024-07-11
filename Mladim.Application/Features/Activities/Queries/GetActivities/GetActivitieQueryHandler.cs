using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Queries.GetActivities;

public class GetActivitieQueryHandler : IRequestHandler<GetActivitiesQuery, IEnumerable<ActivityQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public HttpContext HttpContext { get; }
   
    public GetActivitieQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
        HttpContext = httpContextAccessor.HttpContext;
    } 

    public async Task<IEnumerable<ActivityQueryDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        if (request.ProjectId is int projectId)
        {
            var activities = await this.UnitOfWork.ActivityRepository
                .GetActivitiesWithProjectName(a => a.ProjectId ==  projectId, request.UpcomingActivities);

            return this.Mapper.Map<IEnumerable<ActivityWithProjectNameQueryDto>>(activities);
        }        

        int? orgId = request.OrganizationId.Value;      

        if (orgId is null)
            return Enumerable.Empty<ActivityQueryDto>();

       
       
        if (IsAdminOrManager(this.HttpContext.User.Claims, orgId.Value))
        {
            var activities = await this.UnitOfWork.ActivityRepository
                .GetActivitiesWithProjectName(a => a.Project.OrganizationId == orgId, request.UpcomingActivities);

            return this.Mapper.Map<IEnumerable<ActivityWithProjectNameQueryDto>>(activities);
        }
        else
        {
            var email = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var activities = await this.UnitOfWork.ActivityRepository
                .GetActivitiesWithProjectNameAndStaffMember(a => a.Project.OrganizationId == orgId.Value && a.Project.Staff.Any(s => s.StaffMember.Email == email), request.UpcomingActivities);

            return this.Mapper.Map<IEnumerable<ActivityWithProjectNameQueryDto>>(activities);

        }
               
    }

    private bool IsAdminOrManager(IEnumerable<Claim> claims, int organizationId)
    {
        return claims.Any(c => (c.Type == ClaimTypes.Role && c.Value == nameof(ApplicationRole.Admin)) ||
                         (c.Type == nameof(ApplicationClaim.Manager) && c.Value == organizationId.ToString()));
    }
}
