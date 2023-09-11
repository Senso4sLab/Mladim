using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsByOrganizationQueryHandler : IRequestHandler<GetProjectsByOrganizationQuery, IEnumerable<ProjectQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    public UserManager<AppUser> UserManager { get; }
   
    public GetProjectsByOrganizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
        UserManager = userManager;
    }
   

    public async Task<IEnumerable<ProjectQueryDto>> Handle(GetProjectsByOrganizationQuery request, CancellationToken cancellationToken)
    {
        var user = await this.UserManager.FindByIdAsync(request.UserId);

        ArgumentNullException.ThrowIfNull(user);

        var projects = await GetProjectsByClaimsAsync(user, request.OrganizationId);

        return this.Mapper.Map<IEnumerable<ProjectQueryDto>>(projects);
    }


    private async Task<IEnumerable<Project>> GetProjectsByClaimsAsync(AppUser user, int organizationId)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);
        
        if(IsAdminOrManager(claims,organizationId))
        {
            return  await this.UnitOfWork.ProjectRepository
                .GetAllAsync(p => p.OrganizationId == organizationId);
        }
        else
        {
            return await this.UnitOfWork.ProjectRepository
                .GetAllAsync(p => p.OrganizationId == organizationId && p.Staff.Any(s => s.StaffMember.Email == user.Email));
        }      
    }


    private bool IsAdminOrManager(IEnumerable <Claim> claims, int organizationId)
    {
        return claims.Any(c => (c.ValueType == ClaimTypes.Role && c.Value == nameof(ApplicationRole.Admin)) ||
                         (c.ValueType == nameof(ApplicationClaim.Manager) && c.Value == organizationId.ToString()));
    }
}
