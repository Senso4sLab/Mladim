using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.File;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Extensions;
using Mladim.Domain.Enums;
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

            // add default surveyquestionnairy
            activity.SurveyQuestionnairyId = 1;

            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Partners);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Groups);
            this.UnitOfWork.ConfigEntitiesState(EntityState.Unchanged, activity.Participants);

            // uploaded files
            foreach (var file in request.Files)
            {
                string trustedFileName = await FileApiService.AddFileAsync(file.Data.ToArray(), "Activities", file.FileName);
                activity.Files.Add(AttachedFile.Create(file.FileName, trustedFileName, file.ContentType, "Activities"));
            }
            
            if(request.Attributes.IsRepetitive)
            {
                List<Activity> repetitiveActivities = RepetitiveActivities(activity).ToList();
                project.Activities.AddRange(repetitiveActivities);
            }
            else
                project.Activities.Add(activity);


            return await this.UnitOfWork.SaveChangesAsync() > 0;
        }
        catch (Exception ex) 
        {
            return false;
        }
    }


    private IEnumerable<Activity> RepetitiveActivities(Activity activity)
    {        
        int numOfRepetitions = activity.Attributes.NumOfRepetitions;

        var repetitiveActivity = activity.Clone();

        for (int i = 0; i < numOfRepetitions; i++)
        {            
            repetitiveActivity.ChangeName($"{activity.Attributes.Name} ({i+1}/{numOfRepetitions})");            

            if (i > 0)
            {
                repetitiveActivity.OffsetDateTimeRangeForRepetitiveInterval();
                repetitiveActivity.Files = new List<AttachedFile>();
            }            

            yield return repetitiveActivity;
            repetitiveActivity = repetitiveActivity.Clone();
        }
    }   
}
