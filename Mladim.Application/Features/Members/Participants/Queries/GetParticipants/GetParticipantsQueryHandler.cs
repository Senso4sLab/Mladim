using AutoMapper;
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

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipants;

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<MemberDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<IEnumerable<MemberDto>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Member> members = Enumerable.Empty<Member>();

        if (request.ActivityId is int activityId)
            members = await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(sm => sm.Activities.Any(mp => mp.Id == activityId), request.WithDetails);

        if (request.ProjectId is int projectId)
            members = await this.UnitOfWork.ParticipantRepository.GetParticipantsAsync(sm => sm.Activities.Any(mp => mp.ProjectId == projectId), request.WithDetails);

        return this.Mapper.Map<IEnumerable<MemberDto>>(members);
    }
}
