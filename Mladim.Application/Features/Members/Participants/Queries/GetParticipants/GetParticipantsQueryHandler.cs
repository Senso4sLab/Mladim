using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Features.Members.StaffMembers.Queries.GetStaffMembers;
using Mladim.Domain.Contracts;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipants;

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<INameableEntity>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

    public async Task<IEnumerable<INameableEntity>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<INameableEntity> members = Enumerable.Empty<INameableEntity>();

        if (request.ActivityId is int activityId)
            members = await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.Activities.Any(a => a.Id == activityId), request.IsMemberAbbreviated);

        if (request.ProjectId is int projectId)
            members = await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.Activities.Any(a => a.ProjectId == projectId), request.IsMemberAbbreviated);

        if (request.OrganizationId is int organizationId)
            members = await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(p => p.OrganizationId == organizationId, request.IsMemberAbbreviated);

        return this.Mapper.Map<IEnumerable<INameableEntity>>(members);
    }

    
}
