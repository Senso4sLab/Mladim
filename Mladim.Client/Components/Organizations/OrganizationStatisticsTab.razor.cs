using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Survey;
using System.Runtime.CompilerServices;
using Syncfusion.Blazor.Schedule.Internal;
using Mladim.Client.Models;

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

    //[CascadingParameter]
    //public OrganizationVM? SelectedOrganization { get; set; }

    
    //[Parameter]
    public DefaultOrganization? SelectedOrganization { get; set; }

    private OrganizationStatisticVM organizationStatistics { get; set; }

    private IEnumerable<int> availableYears = new List<int>();  
    private int selectedYear = DateTime.UtcNow.Year;

    private List<ActivityForGantt> activities = new List<ActivityForGantt>();


    private IEnumerable<QuestionResponseStatisticsVM> SurveyStatistics { get; set; } = new List<QuestionResponseStatisticsVM>();

    protected override async Task  OnInitializedAsync()
    {
        SelectedOrganization = await this.OrganizationService.DefaultOrganizationAsync();
    }

    bool isAnyParticipant = false;

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedOrganization != null && !availableYears.Any())
        {   
            availableYears =  this.AvailableYears().ToList();
            await OnOrganizationYearChanged(selectedYear);
        }
        
    }

    public async Task<List<ActivityForGantt>> UpcommingActivitiesAsync(int numOfUpcommingActivities)
    {
        // If upcomming activities is equal ti null all activities will be fetched!
        var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization.Id, numOfUpcommingActivities);

        return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange)).ToList();
    }

    private IEnumerable<int> AvailableYears()
    {
        int createdYear = 2023;//SelectedOrganization!.Attributes.CreatedStamp.Year;
        return Enumerable.Range(createdYear, DateTime.UtcNow.Year - createdYear + 1).ToList();
    }

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
       this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");


    private async Task OnYearChangedAsync(int selectedYear)
    {
        if (this.selectedYear == selectedYear)
            return;

        await OnOrganizationYearChanged(selectedYear);

    }

    private async Task OnOrganizationYearChanged(int selectedYear)
    {
        this.selectedYear = selectedYear;
        this.organizationStatistics = await OrganizationStatisticsAsync(selectedYear);
        this.isAnyParticipant = organizationStatistics?.AgeDoughnut.Count() > 0 && organizationStatistics?.GenderDoughnut.Count() > 0;
        this.activities = await UpcommingActivitiesAsync(5);
        this.SurveyStatistics = await this.SurveyService.GetStatisticsByOrganizationIdAsync(SelectedOrganization.Id, selectedYear);
    }

    public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(int year)
    {
        return await this.OrganizationService.GetStatisticsByYearAsync(SelectedOrganization.Id, year);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }
}