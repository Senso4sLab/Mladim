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

public class AddParticipantCommandHandler : IRequestHandler<AddParticipantCommand, ParticipantDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    

    public AddParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

   
    public async Task<ParticipantDto> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var organization = await this.UnitOfWork.OrganizationRepository
            .FirstOrDefaultAsync(o => o.Id == request.OrganizationId);

        if (organization == null)
            throw new Exception("Organizacija ne obstaja");

        var orgMember = this.Mapper.Map<OrganizationMember>(request);

        organization.Members.Add(orgMember);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<ParticipantDto>(orgMember.Member);      
    }
}
