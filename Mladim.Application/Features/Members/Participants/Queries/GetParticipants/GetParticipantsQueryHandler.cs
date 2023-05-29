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

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IEnumerable<MemberBase>>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   

    public async Task<IEnumerable<MemberBase>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        List<Expression<Func<Participant, bool>>> predicates = new List<Expression<Func<Participant, bool>>>();

        predicates.Add(sm => sm.IsActive == request.IsActive);

        if (request.ActivityId != null)
            predicates.Add(sm => sm.Activities.Any(a => a.Id == request.ActivityId));
        else if (request.OrganizationId != null)
            predicates.Add(sm => sm.OrganizationMembers.Any(om => om.OrganizationId == request.OrganizationId));


        if (request.ParentClass)        
            return await this.UnitOfWork.ParticipantRepository.GetAllAsync<MemberBase>(predicates, p => new MemberBase { Id = p.Id, Name = p.Name });        
        else
        {
            var staff = await this.UnitOfWork.ParticipantRepository.GetAllAsync(predicates);
            return this.Mapper.Map<IEnumerable<ParticipantDetailsQueryDto>>(staff);
        }
    }
}
