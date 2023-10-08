using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Extensions;
using Mladim.Domain.Models;

namespace Mladim.Application.Features.Activities.Commands.AddActivity;

public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, bool>
{
    public IMapper Mapper { get; }
    public IUnitOfWork UnitOfWork { get; }    
    public IFileApiService FileApiService { get; set; }
    public AddActivityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,  IFileApiService apiService) =>
        (UnitOfWork, Mapper, FileApiService) = (unitOfWork, mapper, apiService);

    public async Task<bool> Handle(AddActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var project = await this.UnitOfWork.ProjectRepository
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId);

            ArgumentNullException.ThrowIfNull(project);

            var activity = this.Mapper.Map<Activity>(request);

            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Partners);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Groups);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Participants);

            // uploaded files
            foreach (var file in request.Files)
            {
                string trustedFileName = await FileApiService.AddFileAsync(file.Data.ToArray(), "Activities", file.FileName);
                activity.Files.Add(AttachedFile.Create(file.FileName, trustedFileName, file.ContentType, "Activities"));
            }           

            project.Activities.Add(activity);


            return await this.UnitOfWork.SaveChangesAsync() > 0;
        }
        catch (Exception ex) 
        {
            return false;
        }
    }
}
