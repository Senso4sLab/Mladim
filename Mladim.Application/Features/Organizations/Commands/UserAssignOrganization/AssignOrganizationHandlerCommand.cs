using MediatR;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.UserGetOrganization;

public class AssignOrganizationHandlerCommand : IRequestHandler<AssignOrganizationCommand, bool>
{
    private IUnitOfWork UnitOfWork { get; }
    private IAuthService AuthService { get; }
    public AssignOrganizationHandlerCommand(IUnitOfWork unitOfWork, IAuthService authService)
    {
        UnitOfWork = unitOfWork;
        AuthService = authService;
    } 

    public async Task<bool> Handle(AssignOrganizationCommand request, CancellationToken cancellationToken)
    {  
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var appUser = await this.UnitOfWork.AppUserRepository
            .FirstOrDefaultAsync(u => u.Id == request.AppUserId);

        ArgumentNullException.ThrowIfNull(appUser);

        var existAppUser = await this.UnitOfWork.AppUserRepository
            .FirstOrDefaultAsync(u => u.Organizations.Any(o => o.Id == request.OrganizationId));

        if (existAppUser != null)
            return false;

        appUser.Organizations.Add(organization);

        var claimName = Enum.GetName(request.Claim)!;
        var claim = new Claim(claimName, request.OrganizationId.ToString());

        await this.AuthService.UpsertClaimAsync(appUser, claim);

        return await this.UnitOfWork.SaveChangesAsync() > 0;        
    }
}
