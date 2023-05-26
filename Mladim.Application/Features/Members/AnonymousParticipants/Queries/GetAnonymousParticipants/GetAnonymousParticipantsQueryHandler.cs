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

public class GetAnonymousParticipantsQueryHandler : IRequestHandler<GetAnonymousParticipantsQuery, IEnumerable<AnonymousParticipantDto>>
{
    private IMapper Mapper { get; }
    private IUnitOfWork UnitOfWork { get; }
   
    public GetAnonymousParticipantsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<IEnumerable<AnonymousParticipantDto>> Handle(GetAnonymousParticipantsQuery request, CancellationToken cancellationToken)
    {
        //var ap =  await this.UnitOfWork.AnonymousParticipantRepository.GetAllAsync(x => true, false);

        return this.Mapper.Map<IEnumerable<AnonymousParticipantDto>>(null);   
    }
}
