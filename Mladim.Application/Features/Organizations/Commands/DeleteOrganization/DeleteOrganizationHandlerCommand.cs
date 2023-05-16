using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationHandlerCommand : IRequestHandler<DeleteOrganizationCommand, bool>
{
    private IUnitOfWork UnitOfWork { get; }
    public DeleteOrganizationHandlerCommand(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }  

    public async Task<bool> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = await UnitOfWork.GetRepository<Organization>()
            .GetFirstOrDefaultAsync(o => o.Id == request.OrganizationId,
                o => o.Include(o => o.Projects)
                        .ThenInclude(p => p.ActiveMembers)
                      .Include(o => o.Projects)
                        .ThenInclude(o=> o.Activities)
                            .ThenInclude(a => a.ActiveMember));

        if (organization == null)
            throw new Exception();

        UnitOfWork.GetRepository<Organization>()
            .Remove(organization);

        return await UnitOfWork.SaveChangesAsync() > 0;
    }
}
