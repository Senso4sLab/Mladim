using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Commands.AddStaffMember;
public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public AddStaffMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
   
    public async Task<bool> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {        

        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var organizationMember = this.Mapper.Map<OrganizationMember>(request);
        organization.Members.Add(organizationMember);       

        return await this.UnitOfWork.SaveChangesAsync() > 0;

    }
}
