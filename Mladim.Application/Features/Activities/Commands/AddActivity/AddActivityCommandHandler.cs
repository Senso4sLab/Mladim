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

namespace Mladim.Application.Features.Activities.Commands.AddActivity;

public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, ActivityQueryDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<ActivityQueryDto> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);


        ArgumentNullException.ThrowIfNull(project);

        var partners = request.Partners.Select(o => Partner.Create(o.Id)).ToList();
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, partners);

        var groups = request.Groups.Select(o => ActivityGroup.Create(o.Id)).ToList();
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, groups);


        var participants = request.Participants.Select(o => Participant.Create(o.Id)).ToList();
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, participants);

        var staffMemberRole = request.Staff
            .Select(o => StaffMemberRole.Create(o.StaffMemberId, o.IsLead))
            .ToList();

        var anonymousParticipantGroup = request.AnonymousParticipantActivities
            .Select(a => AnonymousParticipantGroup.Create(a.Number, a.Gender, a.AgeGroup)).ToList();

        var activity = Activity.Create(request.Start, request.End, request.Name, request.Description, request.ActivityTypes,
            partners, staffMemberRole, groups, participants, anonymousParticipantGroup);
           
        project.Activities.Add(activity);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<ActivityQueryDto>(activity);      
    }
}
