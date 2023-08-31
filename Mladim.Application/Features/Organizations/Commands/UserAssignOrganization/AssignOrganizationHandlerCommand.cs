using MediatR;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using System.Security.Claims;

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

        var appUser = await this.UnitOfWork.AppUserRepository.FindByIdAsync(request.AppUserId);           

        ArgumentNullException.ThrowIfNull(appUser);

        var claim = new Claim(Enum.GetName(request.Claim)!, request.OrganizationId.ToString());

        var userExists = await this.UnitOfWork.OrganizationRepository
            .IsUserInOrganizationAsync(request.AppUserId, request.OrganizationId);

        if (userExists)        
            await this.AuthService.ReplaceClaimAsync(appUser, claim);        
        else
        {
            appUser.Organizations.Add(organization);
            await this.AuthService.AddClaimAsync(appUser, claim);
        }
        return await this.UnitOfWork.SaveChangesAsync() > 0;        
    }
}
