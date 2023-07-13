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

namespace Mladim.Application.Features.Members.Participants.Commands.AddParticipant;

public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    

    public AddParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   
    public async Task<bool> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        ArgumentNullException.ThrowIfNull(organization);

        var participant = this.Mapper.Map<Participant>(request);

        await this.UnitOfWork.ParticipantRepository.AddAsync(participant);

        return await this.UnitOfWork.SaveChangesAsync() > 0;      
        
    }
}
