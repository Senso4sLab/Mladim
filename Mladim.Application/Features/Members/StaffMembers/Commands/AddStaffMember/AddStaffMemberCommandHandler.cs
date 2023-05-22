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
public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, StaffMemberDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public AddStaffMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
   
    public async Task<StaffMemberDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
    {        
        if (request.OrganizationId == null)
            throw new Exception("Organizacija ne obstaja");

        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        if (organization == null)
            throw new Exception("Organizacija ne obstaja");

        var orgMember = this.Mapper.Map<OrganizationMember>(request);

        organization.Members.Add(orgMember);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<StaffMemberDto>(orgMember.Member);

    }
}
