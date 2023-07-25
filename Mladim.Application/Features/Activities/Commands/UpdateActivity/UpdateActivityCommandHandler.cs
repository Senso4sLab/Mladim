using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.File;
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
    public IFileApiService FileApiService { get; }

    public UpdateActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileApiService apiService) =>
       (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);

   
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

        // Files 
        activity.Files.Where(f => !request.Files.Any(rf => rf.FileName == f.FileName)).ToList().ForEach(f =>
        {
            FileApiService.DeleteFile(f.StoredFileName, f.FolderName);
            activity.Files.Remove(f);
        });

        request.Files.Where(rf => !activity.Files.Any(f => f.FileName == rf.FileName)).ToList().ForEach(async f =>
        {
            string trustedFileName = await FileApiService.AddFileAsync(f.Data.ToArray(), "Activities", f.FileName);
            activity.Files.Add(AttachedFile.Create(f.FileName, trustedFileName, f.ContentType, "Activities"));
        });

        return await this.UnitOfWork.SaveChangesAsync();
    }
}
