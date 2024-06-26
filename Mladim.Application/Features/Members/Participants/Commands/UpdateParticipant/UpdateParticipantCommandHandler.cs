﻿using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.Participants.Commands.UpdateParticipant;

public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand, int>
{
    public IUnitOfWork UnitOfWork { get; }
    public IMapper Mapper { get; }
    public UpdateParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }


    public async Task<int> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
    {
        var participant = await this.UnitOfWork.ParticipantRepository.FirstOrDefaultAsync(p => p.Id == request.Id);

        ArgumentNullException.ThrowIfNull(participant);

        participant = this.Mapper.Map(request, participant);

        this.UnitOfWork.ParticipantRepository.Update(participant);

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
