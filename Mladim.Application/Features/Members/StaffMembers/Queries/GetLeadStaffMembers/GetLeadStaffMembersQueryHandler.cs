using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetLeadStaffMembers;

public class GetLeadStaffMembersQueryHandler : IRequestHandler<GetLeadStaffMembersQuery, IEnumerable<StaffMemberLeadQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public GetLeadStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<IEnumerable<StaffMemberLeadQueryDto>> Handle(GetLeadStaffMembersQuery request, CancellationToken cancellationToken)
    {
        if(!await this.UnitOfWork.OrganizationRepository.AnyAsync(o => o.Id == request.OrganizationId))
            return new List<StaffMemberLeadQueryDto>();

        var leadStaff = await this.UnitOfWork.StaffMemberRepository.GetLeadStaffMembersAsync(request.OrganizationId);

        return this.Mapper.Map<IEnumerable<StaffMemberLeadQueryDto>>(leadStaff);        
        
    }
}
