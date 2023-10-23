using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.AnonymousParticipants.Queries.GetAnonymousParticipants;

public class GetAnonymousParticipantsQueryHandler : IRequestHandler<GetAnonymousParticipantsQuery, IEnumerable<AnonymousParticipantGroupQueryDto>>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
   
    public GetAnonymousParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<AnonymousParticipantGroupQueryDto>> Handle(GetAnonymousParticipantsQuery request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository.FirstOrDefaultAsync(a => a.Id == request.ActivityId);
        ArgumentNullException.ThrowIfNull(activity);
        return this.Mapper.Map<IEnumerable<AnonymousParticipantGroupQueryDto>>(activity.AnonymousParticipantGroups);
    }
}
