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

namespace Mladim.Application.Features.Activities.Commands.UpdateActivity
{
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
           
            activity.AnonymousParticipants.Where(apa => !request.AnonymousParticipants.Any(apac => apac.AgeGroup == apa.AnonymousParticipant.AgeGroup && apac.Gender == apa.AnonymousParticipant.Gender))
                .ToList().ForEach(apa =>
                {
                    this.UnitOfWork.ConfigEntityState(EntityState.Deleted, apa);
                    activity.AnonymousParticipants.Remove(apa);
                });

            request.AnonymousParticipants.ForEach(apac =>
            {
                var apa = activity.AnonymousParticipants.FirstOrDefault(apa => apac.AgeGroup == apa.AnonymousParticipant.AgeGroup && apac.Gender == apa.AnonymousParticipant.Gender);
                if (apa != null)
                    this.Mapper.Map(apac, apa);
            });

            request.AnonymousParticipants.Where(apa => !activity.AnonymousParticipants.Any(sm => apa.AgeGroup == sm.AnonymousParticipant.AgeGroup && apa.Gender == sm.AnonymousParticipant.Gender))
                 .Select(smc => this.Mapper.Map<AnonymousParticipantActivity>(smc)).ToList().ForEach(aspac =>
                 {
                     this.UnitOfWork.ConfigEntityState(EntityState.Added, aspac);
                     activity.AnonymousParticipants.Add(aspac);
                 });


            activity.Staff.Where(sm => !request.Staff.Any(smc => smc.StaffMemberId == sm.StaffMemberId))
                .ToList().ForEach(rsmp =>
                {
                    this.UnitOfWork.ConfigEntityState(EntityState.Deleted, rsmp);
                    activity.Staff.Remove(rsmp);
                });

            request.Staff.ForEach(s =>
            {
                var smp = activity.Staff.FirstOrDefault(smp => smp.StaffMemberId == s.StaffMemberId);
                if (smp != null)
                    this.Mapper.Map(s, smp);
            });

            request.Staff.Where(smc => !activity.Staff.Any(sm => sm.StaffMemberId == smc.StaffMemberId))
                 .Select(smc => this.Mapper.Map<StaffMemberActivity>(smc)).ToList().ForEach(asmc =>
                 {
                     this.UnitOfWork.ConfigEntityState(EntityState.Added, asmc);
                     activity.Staff.Add(asmc);
                 });

            return await this.UnitOfWork.SaveChangesAsync();
        }
    }
}
