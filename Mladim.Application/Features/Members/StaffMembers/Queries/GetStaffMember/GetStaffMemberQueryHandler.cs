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

namespace Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMember;

public class GetStaffMemberQueryHandler : IRequestHandler<GetStaffMemberQuery, StaffMemberDetailsQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetStaffMemberQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<StaffMemberDetailsQueryDto> Handle(GetStaffMemberQuery request, CancellationToken cancellationToken)
    {
        var staffMember = await this.UnitOfWork.StaffMemberRepository
            .FirstOrDefaultAsync(sm => sm.Id == request.Id);

        ArgumentNullException.ThrowIfNull(staffMember);

        return this.Mapper.Map<StaffMemberDetailsQueryDto>(staffMember);
    }
}
