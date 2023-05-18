﻿using AutoMapper;
using MediatR;
using Mladim.Application.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMember;

public class GetStaffMemberQueryHandler : IRequestHandler<GetStaffMemberQuery, StaffMemberDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetStaffMemberQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<StaffMemberDto> Handle(GetStaffMemberQuery request, CancellationToken cancellationToken)
    {
        var staffMember = await this.UnitOfWork.GetRepository<StaffMember>()
            .GetFirstOrDefaultAsync(sm => sm.Id == request.StaffMemberId);

        if (staffMember == null)
            throw new Exception("");

        return this.Mapper.Map<StaffMemberDto>(staffMember);
    }
}
