using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.DeleteOrganization;

public class RemoveOrganizationHandlerCommand : IRequestHandler<RemoveOrganizationCommand, bool>
{
    private IUnitOfWork UnitOfWork { get; }
    public RemoveOrganizationHandlerCommand(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }  

    public async Task<bool> Handle(RemoveOrganizationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var organization = await this.UnitOfWork.OrganizationRepository
                .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

            ArgumentNullException.ThrowIfNull(organization);

            this.UnitOfWork.OrganizationRepository.Remove(organization);

            return await UnitOfWork.SaveChangesAsync() > 0;
        }
        catch(Exception ex) 
        {
            string message = ex.Message;
            return false;
        }
    }
}
