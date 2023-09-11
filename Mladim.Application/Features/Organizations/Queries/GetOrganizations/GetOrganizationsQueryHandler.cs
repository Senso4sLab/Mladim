using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Mladim.Application.Features.Organizations.Queries.GetOrganizations;

public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<OrganizationQueryDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public UserManager<AppUser> UserManager { get;}
    public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
        this.UserManager = userManager; 
    }

    public async Task<IEnumerable<OrganizationQueryDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    { 
       
        var user = await this.UserManager.FindByIdAsync(request.AppUserId);

        ArgumentNullException.ThrowIfNull(user);

        var organizations = await GetOrganizationsByClaimsAsync(user);

        return organizations == null ? Enumerable.Empty<OrganizationQueryDto>() :
            this.Mapper.Map<IEnumerable<OrganizationQueryDto>>(organizations);        
    }


    private async Task<IEnumerable<Organization>> GetOrganizationsByClaimsAsync(AppUser user)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);

        var isAdmin = claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == nameof(ApplicationRole.Admin));

        if (isAdmin)
        {
            return await this.UnitOfWork.OrganizationRepository
               .GetAllAsync(o => o.AppUsers.Any(a => a.Id == user.Id), false);
        }
        else
        {
            return await this.UnitOfWork.OrganizationRepository
                .GetAllAsync(false);
        }
    }
        
}
