using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, IEnumerable<StaffMemberQueryDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public GetStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }    
 
    public async Task<IEnumerable<StaffMemberQueryDto>> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<INameableEntity> members = Enumerable.Empty<StaffMemberQueryDto>();

        if(request.ActivityId is int activityId)        
            members = await this.UnitOfWork.StaffMemberRepository.GetStaffMembersAsync(sm => sm.StaffActivities.Any(mp => mp.ActivityId == activityId), request.IsMemberAbbreviated);
        
        if (request.ProjectId  is int projectId)        
            members = await this.UnitOfWork.StaffMemberRepository.GetStaffMembersAsync(sm => sm.StaffProjects.Any(mp => mp.ProjectId == projectId), request.IsMemberAbbreviated);
        
        if(request.OrganizationId is  int organizationId)
            members = await this.UnitOfWork.StaffMemberRepository.GetStaffMembersAsync(sm => sm.OrganizationId == organizationId, request.IsMemberAbbreviated);

        return this.Mapper.Map<IEnumerable<StaffMemberQueryDto>>(members);              
    }
}
