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
            .FirstOrDefaultAsync(a => a.Id == request.Id);

        if (activity == null)
            throw new Exception();

        activity = this.Mapper.Map(request, activity);

        activity.Partners.Where(p => !request.Partners.Any(pc => pc.Id == p.Id))
          .ToList().ForEach(rp =>
          {
              this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, rp);
              activity.Partners.Remove(rp);
          });

        request.Partners.Where(pc => !activity.Partners.Any(p => p.Id == pc.Id))
            .Select(apb => this.Mapper.Map<Partner>(apb)).ToList().ForEach(ap =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, ap);
                activity.Partners.Add(ap);
            });

        activity.Participants.Where(p => !request.Participants.Any(pc => pc.Id == p.Id))
          .ToList().ForEach(rp =>
          {
              this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, rp);
              activity.Participants.Remove(rp);
          });

        request.Participants.Where(pc => !activity.Participants.Any(p => p.Id == pc.Id))
            .Select(apb => this.Mapper.Map<Participant>(apb)).ToList().ForEach(ap =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, ap);
                activity.Participants.Add(ap);
            });

        activity.Groups.Where(g => !request.Groups.Any(gc => gc.Id == g.Id))
            .ToList().ForEach(rg =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, rg);
                activity.Groups.Remove(rg);
            });

        request.Groups.Where(gc => !activity.Groups.Any(g => g.Id == gc.Id))
            .Select(gc => this.Mapper.Map<ActivityGroup>(gc)).ToList().ForEach(ag =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, ag);
                activity.Groups.Add(ag);
            });


       // Remove
        activity.AnonymousParticipantActivities.Where(apa => !request.AnonymousParticipantActivities.Any(apac => apac.Id == apa.AnonymousParticipant.Id))
            .ToList().ForEach(apa =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Deleted, apa);
                activity.AnonymousParticipantActivities.Remove(apa);
            });

        // Update potrebno zaradi number property
        request.AnonymousParticipantActivities.ForEach(apac =>
        {
            var apa = activity.AnonymousParticipantActivities.FirstOrDefault(apa => apac.Id == apa.AnonymousParticipant.Id);
            if (apa != null)
                apa.Number = apac.Number;
        });

        // Add
        request.AnonymousParticipantActivities.Where(apa => !activity.AnonymousParticipantActivities.Any(sm => apa.Id == sm.AnonymousParticipant.Id))
              .Select(smc => this.Mapper.Map<AnonymousParticipantActivity>(smc)).ToList().ForEach(aspac =>
               {
                   this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, aspac.AnonymousParticipant);
                   //this.UnitOfWork.ConfigEntityState(EntityState.Added, aspac);
                   activity.AnonymousParticipantActivities.Add(aspac);
               });
      
        activity.Staff.Where(sm => !request.Staff.Any(smc => smc.StaffMemberId == sm.StaffMemberId && smc.IsLead == sm.IsLead))
            .ToList().ForEach(rsmp =>
            {
                this.UnitOfWork.ConfigEntityState(EntityState.Deleted, rsmp);
                activity.Staff.Remove(rsmp);
            });         

        request.Staff.Where(smc => !activity.Staff.Any(sm => sm.StaffMemberId == smc.StaffMemberId && smc.IsLead == sm.IsLead))
             .Select(smc => this.Mapper.Map<StaffMemberActivity>(smc)).ToList().ForEach(asmc =>
             {
                 this.UnitOfWork.ConfigEntityState(EntityState.Added, asmc);
                 activity.Staff.Add(asmc);
             });

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
