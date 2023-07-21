using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Dtos;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Activities.Commands.UpdateActivity;

public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, int>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
    
    public UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        Mapper = mapper;
        UnitOfWork = unitOfWork;
    }       

    public async Task<int> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {   
        var activity = await this.UnitOfWork.ActivityRepository
            .GetActivityDetailsAsync(request.Id);

        ArgumentNullException.ThrowIfNull(activity);

        activity = this.Mapper.Map(request, activity);

        // Partners
        var partner = this.Mapper.Map<IEnumerable<Partner>>(request.Partners);
        activity.Partners.RemoveAll(p => !partner.Any(rp => rp.Equals(p)));

        var addPartners = partner.Where(rp => !activity.Partners.Any(p => p.Equals(rp)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addPartners);
        activity.Partners.AddRange(addPartners);

        //Groups
        var groups = this.Mapper.Map<IEnumerable<ActivityGroup>>(request.Groups);
        activity.Groups.RemoveAll(g => !groups.Any(rp => rp.Equals(g)));

        var addGroups = groups.Where(rg => !activity.Groups.Any(g => g.Equals(rg)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addGroups);
        activity.Groups.AddRange(addGroups);

        // Participants
        var participants = this.Mapper.Map<IEnumerable<Participant>>(request.Participants);
        activity.Participants.RemoveAll(g => !participants.Any(rp => rp.Equals(g)));

        var addParticipants = participants.Where(rg => !activity.Participants.Any(g => g.Equals(rg)));
        this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, addParticipants);
        activity.Participants.AddRange(addParticipants);

        activity.AnonymousParticipantGroups = new(this.Mapper.Map<IEnumerable<AnonymousParticipantGroup>>(request.AnonymousParticipantActivities));

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
