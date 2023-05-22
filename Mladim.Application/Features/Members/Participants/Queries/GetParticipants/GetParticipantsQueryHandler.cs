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

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<ParticipantDto>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<IEnumerable<ParticipantDto>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Participant, bool>> predicate = null;

        if (request.ActivityId != null)
            predicate = sm => sm.Activities.Any(a => a.Id == request.ActivityId);
        else if (request.OrganizationId != null)
            predicate = sm => sm.OrganizationMembers.Any(om => om.OrganizationId == request.OrganizationId);

        if (predicate == null)
            return Enumerable.Empty<ParticipantDto>();

        var participant = await this.UnitOfWork
                .ParticipantRepository.GetAllAsync(predicate);

        return this.Mapper.Map<IEnumerable<ParticipantDto>>(participant);
    }
}
