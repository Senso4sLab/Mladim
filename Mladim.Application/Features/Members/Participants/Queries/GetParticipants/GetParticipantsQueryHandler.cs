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
        IEnumerable<NamedEntity> members = await GetParticipantsAsync(request.ActivityId, request.ProjectId, request.OrganizationId, request.IsMemberAbbreviated);
        return this.Mapper.Map<IEnumerable<NamedEntityDto>>(members);
    }

    private async Task<IEnumerable<NamedEntity>> GetParticipantsAsync(int? activityId, int? projectId, int? organizationId, bool isMemberAbbreviated)
    {
        if (activityId is not null)
            return await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.Activities.Any(a => a.Id == activityId), isMemberAbbreviated);

        if (projectId is not null)
           return await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.Activities.Any(a => a.ProjectId == projectId), isMemberAbbreviated);

        if (organizationId is not null)
          return await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.OrganizationId == organizationId, isMemberAbbreviated);

        return Enumerable.Empty<NamedEntity>();
    }


}
