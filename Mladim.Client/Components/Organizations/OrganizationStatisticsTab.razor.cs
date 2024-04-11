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


    

    SfAccumulationChart accChart;

     ElementReference Element;

    //[CascadingParameter]
    //public OrganizationVM? SelectedOrganization { get; set; }


    //[Parameter]
    public DefaultOrganization? SelectedOrganization { get; set; }

    private OrganizationStatisticVM organizationStatistics { get; set; }

    //private IEnumerable<int> availableYears = new List<int>();  
    //private int selectedYear = DateTime.UtcNow.Year;

    private DateRange statisticsDateRange = new DateRange();





    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private string chartWidth = "100%";
    private IEnumerable<QuestionSurveyStatisticsVM> SurveyStatistics { get; set; } = new List<QuestionSurveyStatisticsVM>();

    protected override async Task OnInitializedAsync()
    {
        SelectedOrganization = await this.OrganizationService.DefaultOrganizationAsync();
        SetDefaultOrgStatisticsDateRange(DateTime.UtcNow);
        await OrgStatisticsDateTimePickerClosed();
    }

    private async Task Export()
    {
        
    }

    


    private void SetDefaultOrgStatisticsDateRange(DateTime now)
    {
        statisticsDateRange = new DateRange(now.AddYears(-1), now);
    }



    bool isAnyParticipant = false;   

   
    private async Task GeneratePdf()
    {
        chartWidth = "680px";

        //Task.Delay(1000).ContinueWith(async x =>
        //{
        //    await JS.InvokeVoidAsync("extractHtmlAndPrint", "org_statistic_id");
        //    chartWidth = "100%";
        //    StateHasChanged();
        //});

        //await accChart.ExportAsync(Syncfusion.Blazor.Charts.ExportType.PNG, "test", null, false);
        await accChart.PrintAsync(Element);
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

        this.SurveyStatistics = await this.SurveyService.GetStatisticsByOrganizationIdAsync(SelectedOrganization.Id, statisticsDateRange.Start.Value, statisticsDateRange.End.Value);


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