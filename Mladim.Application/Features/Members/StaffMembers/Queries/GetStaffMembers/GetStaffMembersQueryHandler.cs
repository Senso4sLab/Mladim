using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
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

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, IEnumerable<MemberDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public GetStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    
   
    public async Task<IEnumerable<MemberDto>> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Member> members = Enumerable.Empty<Member>();   

        if(request.ActivityId is int activityId)        
            members = await this.UnitOfWork.StaffMemberRepository.GetAllAsync(sm => sm.StaffActivities.Any(mp => mp.ActivityId == activityId), false);
        
        if (request.ProjectId  is int projectId)        
            members = await this.UnitOfWork.StaffMemberRepository.GetAllAsync(sm => sm.StaffProjects.Any(mp => mp.ProjectId == projectId), false);        

        return this.Mapper.Map<IEnumerable<MemberDto>>(members);              
    }
}
