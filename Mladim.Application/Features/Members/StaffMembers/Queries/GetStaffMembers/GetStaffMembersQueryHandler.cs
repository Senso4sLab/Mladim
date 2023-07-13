using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Mladim.Application.Contracts.Persistence;

using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, IEnumerable<NamedEntityDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public GetStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }    
 
    public async Task<IEnumerable<NamedEntityDto>> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NamedEntity> members = await GetStaffMembersAsync(request);
        return this.Mapper.Map<IEnumerable<NamedEntityDto>>(members);              
    }


    private async Task<IEnumerable<NamedEntity>> GetStaffMembersAsync(GetStaffMembersQuery request)
    {
        if (request.ActivityId is int activityId)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.IsActive == request.IsActive && sm.StaffActivities.Any(mp => mp.ActivityId == activityId), request.IsMemberAbbreviated);

        if (request.ProjectId is int projectId)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.IsActive == request.IsActive && sm.StaffProjects.Any(mp => mp.ProjectId == projectId), request.IsMemberAbbreviated);

        if (request.OrganizationId is int organizationId)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.IsActive == request.IsActive && sm.OrganizationId == organizationId, request.IsMemberAbbreviated);

        return Enumerable.Empty<NamedEntity>();
    }
}
