using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Project;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Models;
using static MudBlazor.CategoryTypes;
using Mladim.Domain.Models;

namespace Mladim.Client.Components.Organizations;

public partial class ProjectStatisticsTab : IExportChart
{
    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;


    [Inject]
    public IOrganizationService OrganizationService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;    
       

    [Parameter]
    public bool PastActivities { get; set; }   

    public string StackedBarWidth { get; set; } = "100%";

    List<Func<Task>> ExportChartsAsync = new List<Func<Task>>();


    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private List<ProjectVM> selectedProjects = new List<ProjectVM>(); 

    private ProjectStatisticsVM? projectStatistics;

    private string activitiesText = string.Empty;

    private IEnumerable<QuestionSurveyStatisticsVM> surveyStatistics = new List<QuestionSurveyStatisticsVM>();


    private IEnumerable<ProjectVM> Projects = new List<ProjectVM>();

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
      this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");
       
    private async Task<int> GetOrganizationIdAsync()
    {
        var organization = await this.OrganizationService.DefaultOrganizationAsync();
        return organization!.Id;
    }

    
    protected override async Task  OnInitializedAsync()
    {       
        Projects = await ProjectsByOrganizationAsync(await GetOrganizationIdAsync());

        var dateTime = DateTime.UtcNow;

        if (PastActivities)
            Projects = Projects.Where(p => p.IsCompleted(dateTime)).ToList();
        else
        {
            Projects = Projects.Where(p => !p.IsCompleted(dateTime)).ToList();
            activitiesText = "prihajajočih";
        }

        if (Projects.FirstOrDefault() is ProjectVM project)        
            selectedProjects.Add(project);
            
        
                
    }

    

  

    public async Task<IEnumerable<ProjectVM>> ProjectsByOrganizationAsync(int organizationId)
    {
        return await this.ProjectService.GetByOrganizationIdAsync(organizationId);
    }

    private async Task OnChangedSelectedProjectAsync(int projectId)
    {
               
        projectStatistics = await ProjectStatisticsAsync(projectId);
        activities = await ActivitiesAsync(projectId);
        surveyStatistics = await SurveyService.GetStatisticsByProjectIdIdAsync(projectId);
       
    }

    public void AddExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Add(exportChart);
    }

    public void RemoveExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Remove(exportChart);
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


    private async Task OnProjectsSelectionChangedAsync(IEnumerable<ProjectVM> projects)
    {
        selectedProjects = projects.ToList();
        await OnChangedSelectedProjectAsync(selectedProjects.FirstOrDefault().Id);
      
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }

    private async Task GeneratePdfAsync()
    {
        StackedBarWidth = "800px";
        await Task.Delay(100);
        //await ParticipantsByAgeChart.PrintAsync(Element);
        StackedBarWidth = "100%";
    }

}