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

namespace Mladim.Application.Features.Members.Participants.Queries.GetParticipant;

public class GetParticipantQueryHandler : IRequestHandler<GetParticipantQuery, ParticipantDetailsQueryDto>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public GetParticipantQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    

    public async Task<ParticipantDetailsQueryDto> Handle(GetParticipantQuery request, CancellationToken cancellationToken)
    {
        var participant = await this.UnitOfWork.ParticipantRepository
            .FirstOrDefaultAsync(sm => sm.Id == request.Id);

        ArgumentNullException.ThrowIfNull(participant);

        return this.Mapper.Map<ParticipantDetailsQueryDto>(participant);
    }
}
