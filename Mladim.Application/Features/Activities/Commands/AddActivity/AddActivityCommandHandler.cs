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
            .FirstOrDefaultWithoutIncludeAsync(p => p.Id == request.ProjectId);            
          
        if (project == null)
            throw new Exception();
        try
        {


            var activity = this.Mapper.Map<Activity>(request);

            if (activity == null)
                throw new Exception();


            this.UnitOfWork.ConfigEntityState<Partner>(EntityState.Unchanged, activity.Partners);
            this.UnitOfWork.ConfigEntityState<ActivityGroup>(EntityState.Unchanged, activity.Groups);
            this.UnitOfWork.ConfigEntityState<Participant>(EntityState.Unchanged, activity.Participants);

            //foreach (var anonymousParticipant in activity.AnonymousParticipants)
            //    this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, anonymousParticipant.AnonymousParticipant);

            //foreach(var ap in request.AnonymousParticipants)
            //{
            //    var par = await UnitOfWork.AnonymousParticipantRepository
            //        .FirstOrDefaultAsync(p => p.AgeGroup == ap.AgeGroup && p.Gender == ap.Gender);

            //    activity.AnonymousParticipants.Add(new AnonymousParticipantActivity
            //    {
            //        AnonymousParticipantId = par.Id,

            //    }) ;
            //}           

            foreach(var anonymousParticipant in activity.AnonymousParticipantActivities)
                this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, anonymousParticipant.AnonymousParticipant);
            


            project.Activities.Add(activity);

            await this.UnitOfWork.SaveChangesAsync();

            return this.Mapper.Map<ActivityQueryDto>(activity);

        }
        catch (Exception ex)
        {
            string message = ex.Message;

            return null;           
        }
    }
}
