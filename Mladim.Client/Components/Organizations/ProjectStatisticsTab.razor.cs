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
using Syncfusion.Blazor.Charts;

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
    bool isActiveExportingImages = false;

    SfAccumulationChart ParticipantsByAgeChart = default!;
    SfAccumulationChart ParticipantsByGenderChart = default!;
    ElementReference Element;

    private List<QuestionSurveyStatisticsVM> surveyStatistics = new List<QuestionSurveyStatisticsVM>();


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

        ExportChartsAsync.Add(() => ExportAccumulationChartToImage(ParticipantsByAgeChart));
        ExportChartsAsync.Add(() => ExportAccumulationChartToImage(ParticipantsByGenderChart));
    }


    private Task ExportAccumulationChartToImage(SfAccumulationChart? chart) =>
        chart?.ExportAsync(Syncfusion.Blazor.Charts.ExportType.PNG, Guid.NewGuid().ToString()) ?? Task.CompletedTask;


    public async Task<IEnumerable<ProjectVM>> ProjectsByOrganizationAsync(int organizationId)
    {
        return await this.ProjectService.GetByOrganizationIdAsync(organizationId);
    }

    private async Task OnChangedSelectedProjectAsync(int projectId)
    {        
        activities = await ActivitiesAsync(projectId);
        projectStatistics = await ProjectStatisticsAsync(projectId);   
        
        surveyStatistics.Clear();
        StateHasChanged();
        surveyStatistics = new (await SurveyService.GetStatisticsByProjectIdIdAsync(projectId));
       
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
        await OnChangedSelectedProjectAsync(projects.FirstOrDefault().Id);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }

    private async Task GeneratePdfAsync()
    {
        StackedBarWidth = "800px";
        await Task.Delay(100);
        await ParticipantsByAgeChart.PrintAsync(Element);
        StackedBarWidth = "100%";
        StateHasChanged();
    }

    public async Task GenerateImagesAsync()
    {
        isActiveExportingImages = true;

        foreach (var chunk in ExportChartsAsync.Chunk(6))
        {
            await Task.WhenAll(chunk.Select(x => x.Invoke()));
            await Task.Delay(1000);
        }

        isActiveExportingImages = false;
    }

}