using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public HttpContext HttpContext { get; }
    public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        this.Mapper = mapper;
        this.UnitOfWork = unitOfWork;
        this.HttpContext = httpContextAccessor.HttpContext; 
    }

    public async Task<IEnumerable<OrganizationQueryDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var claims = this.HttpContext.User.Claims;

        if (UserIdFromClaim(claims) != request.AppUserId)
            throw new OperationCanceledException();

        var organizations = await GetOrganizationsByClaimsAsync(claims, request.AppUserId);

        return organizations == null ? Enumerable.Empty<OrganizationQueryDto>() :
            this.Mapper.Map<IEnumerable<OrganizationQueryDto>>(organizations);        
    }


    private async Task<IEnumerable<Organization>> GetOrganizationsByClaimsAsync(IEnumerable<Claim> claims, string userId)
    {
        var isAdmin = claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == nameof(ApplicationRole.Admin));

        if (isAdmin)
        {
            return await this.UnitOfWork.OrganizationRepository
                .GetAllAsync(false);          
        }
        else
        {           
            return await this.UnitOfWork.OrganizationRepository
                .GetAllWithAppUser(userId);          
        }
    }


    private string UserIdFromClaim(IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
        
}
