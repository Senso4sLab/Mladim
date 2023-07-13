using AutoMapper;
using AutoMapper.Execution;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;

using Mladim.Domain.Dtos;
using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipants;

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<NamedEntityDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<IEnumerable<NamedEntityDto>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NamedEntity> members = await GetParticipantsAsync(request);
        return this.Mapper.Map<IEnumerable<NamedEntityDto>>(members);
    }

    private async Task<IEnumerable<NamedEntity>> GetParticipantsAsync(GetParticipantsQuery request)
    {
        if (request.ActivityId is int activityId)
            return await this.UnitOfWork.ParticipantRepository.
                GetParticipantsAsync(p => p.IsActive == request.IsActive && p.Activities.Any(a => a.Id == activityId), request.IsMemberAbbreviated);

        if (request.ProjectId is int projectId)
           return await this.UnitOfWork.ParticipantRepository.
                GetParticipantsAsync(p => p.IsActive == request.IsActive && p.Activities.Any(a => a.ProjectId == projectId), request.IsMemberAbbreviated);

        if (request.OrganizationId is int organizationId)
          return await this.UnitOfWork.ParticipantRepository.
                GetParticipantsAsync(p => p.IsActive == request.IsActive && p.OrganizationId == organizationId, request.IsMemberAbbreviated);

        return Enumerable.Empty<NamedEntity>();
    }


}
