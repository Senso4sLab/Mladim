﻿using AutoMapper;
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
       
        activity.PeriodOfImplementation(request.Start, request.End);        
        activity.SetBaseAttributes(request.Name, request.Description, request.ActivityTypes);

        // Partners
        var otherPartners = request.Partners
            .Select(other => Partner.Create(other.Id))
            .ToList();

        // Add Partners
        var partnersToAdd = otherPartners
            .Where(p => !activity.Exists(p))
            .ToList();

        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, partnersToAdd);
        activity.AddRange(partnersToAdd);

        // Remove Partners
        var removePartners = activity.Partners
            .Where(p => !otherPartners.Any(rp => rp == p))
            .ToList();

        activity.RemoveRange(removePartners);

        // Groups
        var otherGroups = request.Groups
            .Select(other => ActivityGroup.Create(other.Id))
            .ToList();

        // Add Groups
        var groupsToAdd = otherGroups
            .Where(p => !activity.Exists(p))
            .ToList();

        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, groupsToAdd);
        activity.AddRange(groupsToAdd);

        // Remove Groups
        var removeGroups = activity.Groups
            .Where(g => !otherGroups.Any(rg => rg == g))
            .ToList();

        activity.RemoveRange(removeGroups);

        // StaffRole
        var otherStaff = request.Staff
            .Select(other => StaffMemberRole.Create(other.StaffMemberId, other.IsLead))
            .ToList();

        //Remove Staff
        var removeStaff = activity.Staff
            .Where(smp => !otherStaff.Any(o => o.StaffMember == smp.StaffMember))
            .ToList();

        this.UnitOfWork.ConfigEntityState(EntityState.Deleted, removeStaff);
        activity.RemoveRange(removeStaff);

        // Modify StaffMemberProjects              
        otherStaff.ForEach(activity.SetStaffMemberRole);

        // Add StaffMemberProjects
        var addStaffMembers = otherStaff
           .Where(sm => !activity.Exists(sm.StaffMember))
           .ToList();

        activity.AddRange(addStaffMembers);

        // Participants
        var otherParticipants = request.Participants
            .Select(other => Participant.Create(other.Id))
            .ToList();

        // Add Participants
        var participantsToAdd = otherParticipants
            .Where(p => !activity.Exists(p))
            .ToList();

        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, participantsToAdd);
        activity.AddRange(participantsToAdd);

        // Remove Participants
        var removeParticipants = activity.Participants
            .Where(g => !otherParticipants.Any(rg => rg == g))
            .ToList();

        activity.RemoveRange(removeParticipants);

        // AnonymousParticipants

        var otherAnonymousParticipantGroups = request.AnonymousParticipantActivities
            .Select(other => AnonymousParticipantGroup.Create(other.Number, other.Gender, other.AgeGroup))
            .ToList();

        // Remove AnonymousParticipants
        var removeAnonymousParticipants = activity.AnonymousParticipantGroups
           .Where(ag => !otherAnonymousParticipantGroups.Any(rg => rg == ag))
           .ToList();

        activity.RemoveRange(removeAnonymousParticipants);

        // Add AnonymousParticipants
        var addAnonymousParticipants = otherAnonymousParticipantGroups
          .Where(apg => !activity.Exists(apg))
          .ToList();

        activity.AddRange(addAnonymousParticipants);       

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
