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

public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, ActivityDto>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }
   
    public AddActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }   

    public async Task<ActivityDto> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        var project = await this.UnitOfWork.ProjectRepository
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId);            
          

        if (project == null)
            throw new Exception();

        var activity = this.Mapper.Map<Activity>(request);

        this.UnitOfWork.ConfigEntityState(activity.Partners, EntityState.Unchanged);
        this.UnitOfWork.ConfigEntityState(activity.Groups, EntityState.Unchanged);
        this.UnitOfWork.ConfigEntityState(activity.Staff.Select(sp => sp.StaffMember), EntityState.Unchanged);
        this.UnitOfWork.ConfigEntityState(activity.Participants, EntityState.Unchanged);
        this.UnitOfWork.ConfigEntityState(activity.AnonymousParticipants.Select(ap => ap.AnonymousParticipant), EntityState.Unchanged);        

        project.Activities.Add(activity);

        await this.UnitOfWork.SaveChangesAsync();

        return this.Mapper.Map<ActivityDto>(activity);   
    }
}
