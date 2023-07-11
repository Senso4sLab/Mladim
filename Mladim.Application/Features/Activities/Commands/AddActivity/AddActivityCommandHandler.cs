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

public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<bool> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

        ArgumentNullException.ThrowIfNull(project);

        var activity = this.Mapper.Map<Activity>(request);
       
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, activity.Partners);      
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, activity.Groups);       
        this.UnitOfWork.ConfigEntityState(EntityState.Unchanged, activity.Participants);
        
        //var anonymousParticipantGroup = request.AnonymousParticipantActivities
        //    .Select(a => AnonymousParticipantGroup.Create(a.Number, a.Gender, a.AgeGroup)).ToList();
                   
        project.Activities.Add(activity);

        return await this.UnitOfWork.SaveChangesAsync() > 0;
    }
}
