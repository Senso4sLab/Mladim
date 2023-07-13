using AutoMapper;
using MediatR;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Members.AnonymousParticipants.Commands.AddAnonymousParticipant;

public class AddAnonymousParticipantCommandHandler : IRequestHandler<AddAnonymousParticipantCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }

    public AddAnonymousParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    public async Task<bool> Handle(AddAnonymousParticipantCommand request, CancellationToken cancellationToken)
    {
        var activity = await this.UnitOfWork.ActivityRepository
            .FirstOrDefaultAsync(a => a.Id == request.ActivityId);

        ArgumentNullException.ThrowIfNull(activity);

        var anonymousParticipantGroup = this.Mapper.Map<AnonymousParticipantGroup>(request);

        ArgumentNullException.ThrowIfNull(anonymousParticipantGroup);

        activity.Add(anonymousParticipantGroup);

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
