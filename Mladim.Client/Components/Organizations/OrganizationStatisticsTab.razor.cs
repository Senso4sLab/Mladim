using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Survey;
using Mladim.Client.Models;
using Microsoft.JSInterop;
using System.Timers;
using MudBlazor;
using Syncfusion.Blazor.Charts;
using Mladim.Domain.Models.Survey.Statistics;


namespace Mladim.Client.Components.Organizations;

public partial class OrganizationStatisticsTab
{

    [Inject]
    public IOrganizationService OrganizationService { get; set; } = default!;

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public ISurveyService SurveyService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    bool MoreQuestionStatistics { get; set; } = false;
    IEnumerable<int> defaultQuestionsForStatistics = new List<int>() { 1, 2, 3, 4, 5, 11 };


    bool isActiveExportingImages = false;
    bool isAnyParticipant = false;

    SfAccumulationChart accChart = default!;
    ElementReference Element;
    


    List<Func<Task>> ExportChartsAsync = new List<Func<Task>>();
    public DefaultOrganization? SelectedOrganization { get; set; }
    private OrganizationStatisticVM organizationStatistics { get; set; } = default!;

  

    private DateRange statisticsDateRange = new DateRange();
    
    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    public string stackedBarWidth = "100%";
    private List<QuestionSurveyStatisticsVM> ShownQuestionsSurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();

    private IEnumerable<QuestionSurveyStatisticsVM> QuestionsSurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();

    protected override async Task OnInitializedAsync()
    {
        SelectedOrganization = await this.OrganizationService.DefaultOrganizationAsync();
        SetDefaultOrgStatisticsDateRange(DateTime.UtcNow);
        await OrgStatisticsDateTimePickerClosed();
       
    }

    public void OnMoreQuestionStatisticsChanged(bool toggled)
    {
        MoreQuestionStatistics = !MoreQuestionStatistics;
        ShowingQuestionsForSurveyStatistics();    
    }

    public void AddExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Add(exportChart);    
    }

    public void RemoveExportChart(Func<Task> exportChart)
    {
        this.ExportChartsAsync.Remove(exportChart);
    }
    private void SetDefaultOrgStatisticsDateRange(DateTime now)
    {
        statisticsDateRange = new DateRange(now.AddYears(-1), now);
    }  
  
    private async Task GeneratePdfAsync()
    {
        stackedBarWidth = "680px";        
        await Task.Delay(100);
        await accChart.PrintAsync(Element);
        stackedBarWidth = "100%";       
    }


    public async Task GenerateImagesAsync()
    {
        isActiveExportingImages = true;       
        await Task.WhenAll(ExportChartsAsync.Select(x => x.Invoke()));
        isActiveExportingImages = false;
    }

    public async Task<List<ActivityForGantt>> UpcommingActivitiesAsync(int numOfUpcommingActivities)
    {
        // If upcomming activities is equal ti null all activities will be fetched!
        var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization.Id, numOfUpcommingActivities);

        return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange)).ToList();
    }

    

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
       this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");


    private async Task OrgStatisticsDateTimePickerClosed()
    {
        this.organizationStatistics = await OrganizationStatisticsAsync(statisticsDateRange);
        this.isAnyParticipant = organizationStatistics?.AgeDoughnut.Count() > 0 && organizationStatistics?.GenderDoughnut.Count() > 0;

        this.activities = await UpcommingActivitiesAsync(5);

        this.QuestionsSurveyStatistics = await this.SurveyService.GetStatisticsByOrganizationIdAsync(SelectedOrganization.Id, statisticsDateRange.Start.Value, statisticsDateRange.End.Value);
        this.MoreQuestionStatistics = false;   

        ShownQuestionsSurveyStatistics = QuestionsSurveyStatistics.IntersectBy(defaultQuestionsForStatistics, qs => qs.SurveyQuestion.UniqueQuestionId).ToList();    

        ShowingQuestionsForSurveyStatistics();     
    }

    private void ShowingQuestionsForSurveyStatistics()
    {

        var questionSurveyStatistics = QuestionsSurveyStatistics.ExceptBy(defaultQuestionsForStatistics, qs => qs.SurveyQuestion.UniqueQuestionId).ToList();

        if (MoreQuestionStatistics)        
            ShownQuestionsSurveyStatistics.AddRange(questionSurveyStatistics);       
        else
        {
            foreach (var question in questionSurveyStatistics)           
                ShownQuestionsSurveyStatistics.Remove(question);           
        }        
    }   
    
    public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(DateRange range)
    {
        return await this.OrganizationService.GetStatisticsByDateRangeAsync(SelectedOrganization!.Id, range.Start!.Value, range.End!.Value);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }
}