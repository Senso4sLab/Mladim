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
    public IJSRuntime JS { get; set; }

    bool MoreQuestionStatistics { get; set; } = false;
    IEnumerable<int> DefaultQuestionsForStatistics = new List<int>() { 1, 2, 3, 4, 5, 11 };


    SfAccumulationChart accChart;

     ElementReference Element;

    
    public DefaultOrganization? SelectedOrganization { get; set; }

    private OrganizationStatisticVM organizationStatistics { get; set; } = default!;

  

    private DateRange statisticsDateRange = new DateRange();
    
    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private string chartWidth = "100%";
    private IEnumerable<QuestionSurveyStatisticsVM> ShownQuestionsSurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();

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
        ShownQuestionsSurveyStatistics = ShowingQuestionsForSurveyStatistics();    
    }




    private void SetDefaultOrgStatisticsDateRange(DateTime now)
    {
        statisticsDateRange = new DateRange(now.AddYears(-1), now);
    }



    bool isAnyParticipant = false;

    System.Action ExportCharts { get;set; }
   
  
    private async Task GeneratePdf()
    {
        //chartWidth = "680px";      

        ////await accChart.ExportAsync(Syncfusion.Blazor.Charts.ExportType.PNG, "test", null, false);

        //await Task.Delay(100);

        //await accChart.PrintAsync(Element);

        //chartWidth = "100%";

        ExportCharts += () => Console.WriteLine("test");

        if (ExportCharts == null)
        {

        }


        ExportCharts?.Invoke();
        //Actionn();
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
        this.ShownQuestionsSurveyStatistics = ShowingQuestionsForSurveyStatistics();     
    }

    private IEnumerable<QuestionSurveyStatisticsVM> ShowingQuestionsForSurveyStatistics() =>
        this.MoreQuestionStatistics ? 
        this.QuestionsSurveyStatistics.ToList() : 
        this.QuestionsSurveyStatistics.Where(qss => DefaultQuestionsForStatistics.Any(q => q == qss.SurveyQuestion.UniqueQuestionId)).ToList();


    

   
    
    public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(DateRange range)
    {
        return await this.OrganizationService.GetStatisticsByDateRangeAsync(SelectedOrganization!.Id, range.Start!.Value, range.End!.Value);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }
}