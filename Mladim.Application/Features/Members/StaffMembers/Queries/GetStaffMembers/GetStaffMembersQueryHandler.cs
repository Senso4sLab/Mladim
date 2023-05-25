﻿using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

public class GetStaffMembersQueryHandler : IRequestHandler<GetStaffMembersQuery, IEnumerable<StaffMemberDto>>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public GetStaffMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
	{
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    

    public async Task<IEnumerable<StaffMemberDto>> Handle(GetStaffMembersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<StaffMember, bool>> predicate = null;

        if (request.ActivityId != null)
            predicate = sm => sm.IsActive == request.IsActive && sm.StaffActivities.Any(mp => mp.ActivityId == request.ActivityId);
        else if (request.ProjectId != null)
            predicate = sm => sm.IsActive == request.IsActive && sm.StaffProjects.Any(mp => mp.ProjectId == request.ProjectId);
        else if (request.OrganizationId != null)
            predicate = sm => sm.IsActive == request.IsActive && sm.OrganizationMembers.Any(om => om.OrganizationId == request.OrganizationId);        
        
        if(predicate == null)
            return Enumerable.Empty<StaffMemberDto>();

        var staff = await this.UnitOfWork
                .StaffMemberRepository.GetAllAsync(predicate);

        return this.Mapper.Map<IEnumerable<StaffMemberDto>>(staff);
    }
}
