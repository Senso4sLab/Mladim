using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Project;
using Mladim.Client.ViewModels.Survey;

namespace Mladim.Client.Components.Organizations;

public partial class ProjectStatisticsTab
{
    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;    
       

    [Parameter]
    public bool PastActivities { get; set; }

    [Parameter]
    public IEnumerable<ProjectVM> Projects { get; set; } = new List<ProjectVM>();


    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private ProjectVM? selectedProject;

    private ProjectStatisticsVM? projectStatistics;


    private IEnumerable<QuestionSurveyStatisticsVM> surveyStatistics = new List<QuestionSurveyStatisticsVM>();  

    //private List<DoughnutPiece> GenderDoughnut = new List<DoughnutPiece>();
    //private List<DoughnutPiece> AgeDoughnut = new List<DoughnutPiece>();

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
      this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");
       

    protected override async Task  OnInitializedAsync()
    {
        selectedProject = Projects.FirstOrDefault();
        
        if (selectedProject == null)
            return;

        await OnChangedSelectedProjectAsync(selectedProject.Id);
    }

    private async Task OnChangedSelectedProjectAsync(int projectId)
    {
        projectStatistics = await ProjectStatisticsAsync(projectId);
        activities = await ActivitiesAsync(projectId);
        surveyStatistics = await SurveyService.GetStatisticsByProjectIdIdAsync(projectId);
    }


    private async Task<List<ActivityForGantt>> ActivitiesAsync(int projectId)
    {
        var activities = await this.ActivityService.GetByProjectIdAsync(projectId, this.PastActivities ? null : 5);        

        return activities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange)).ToList();
    }

    private async Task<ProjectStatisticsVM?> ProjectStatisticsAsync(int projectId)
    {
        return await this.ProjectService.GetStatisticsAsync(projectId);
    }


    private async Task OnProjectChangedAsync(ProjectVM project)
    {
        if (selectedProject.Id != project.Id)
        {
            selectedProject = project;
            await OnChangedSelectedProjectAsync(selectedProject.Id);
        }
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }

}