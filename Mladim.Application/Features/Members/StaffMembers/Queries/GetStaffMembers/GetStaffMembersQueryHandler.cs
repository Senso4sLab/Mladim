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

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, IEnumerable<MemberBase>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public GetStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    
   
    public async Task<IEnumerable<MemberBase>> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<StaffMember, bool>>> predicates = new List<Expression<Func<StaffMember, bool>>>();

        predicates.Add(sm => sm.IsActive == request.IsActive);

        if (request.ActivityId != null)           
            predicates.Add(sm => sm.StaffActivities.Any(mp => mp.ActivityId == request.ActivityId));        
        else if (request.ProjectId != null)        
            predicates.Add(sm => sm.StaffProjects.Any(mp => mp.ProjectId == request.ProjectId));       
        else if (request.OrganizationId != null)
            predicates.Add(sm => sm.OrganizationMembers.Any(om => om.OrganizationId == request.OrganizationId));


        if (request.ParentClass)        
            return await this.UnitOfWork.StaffMemberRepository.GetAllAsync<MemberBase>(predicates, sm => new MemberBase { Id = sm.Id, Name = sm.Name});        
        else
        {
            var staff = await this.UnitOfWork.StaffMemberRepository.GetAllAsync(predicates);
            return this.Mapper.Map<IEnumerable<StaffMemberDetailsQueryDto>>(staff);
        }              
    }
}
