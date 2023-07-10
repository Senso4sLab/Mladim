using MediatR;
using Mladim.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.UserGetOrganization;

public class AssignOrganizationHandlerCommand : IRequestHandler<AssignOrganizationCommand, bool>
{
    private IUnitOfWork UnitOfWork { get; }
    public AssignOrganizationHandlerCommand(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
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
        return await this.UnitOfWork.SaveChangesAsync() > 0;        
    }
}
