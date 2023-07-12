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
        IEnumerable<NamedEntity> members = await GetStaffMembersAsync(request.ActivityId, request.ProjectId, request.OrganizationId, request.IsMemberAbbreviated);
        return this.Mapper.Map<IEnumerable<NamedEntityDto>>(members);              
    }


    private async Task<IEnumerable<NamedEntity>> GetStaffMembersAsync(int? activityId, int? projectId, int? organizationId, bool isMemberAbbreviated)
    {
        if (activityId is not null)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.StaffActivities.Any(mp => mp.ActivityId == activityId), isMemberAbbreviated);

        if (projectId is not null)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.StaffProjects.Any(mp => mp.ProjectId == projectId), isMemberAbbreviated);

        if (organizationId is not null)
            return await this.UnitOfWork.StaffMemberRepository
                .GetStaffMembersAsync(sm => sm.OrganizationId == organizationId, isMemberAbbreviated);

        return Enumerable.Empty<NamedEntity>();
    }
}
