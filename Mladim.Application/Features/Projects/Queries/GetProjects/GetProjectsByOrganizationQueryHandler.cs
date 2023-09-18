using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public HttpContext HttpContext { get; set; }


    public GetProjectsByOrganizationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;       
      
        HttpContext = httpContextAccessor!.HttpContext;
    }
   

    public async Task<IEnumerable<ProjectQueryDto>> Handle(GetProjectsByOrganizationQuery request, CancellationToken cancellationToken)
    {      

        var projects = await GetProjectsByClaimsAsync(this.HttpContext.User.Claims, request.OrganizationId);

        return this.Mapper.Map<IEnumerable<ProjectQueryDto>>(projects);
    }


    private async Task<IEnumerable<Project>> GetProjectsByClaimsAsync(IEnumerable<Claim> claims, int organizationId)
    {    
        
        if(IsAdminOrManager(claims,organizationId))
        {
            return  await this.UnitOfWork.ProjectRepository
                .GetAllAsync(p => p.OrganizationId == organizationId);
        }
        else
        {
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            return  await this.UnitOfWork.ProjectRepository.GetProjectsWithStaffMemberWithAsync(organizationId, email);           
        }      
    }


    private bool IsAdminOrManager(IEnumerable <Claim> claims, int organizationId)
    {
        return claims.Any(c => (c.Type == ClaimTypes.Role && c.Value == nameof(ApplicationRole.Admin)) ||
                         (c.Type == nameof(ApplicationClaim.Manager) && c.Value == organizationId.ToString()));
    }
}
