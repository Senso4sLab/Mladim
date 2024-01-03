using global::Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.ViewModels.Organization;

namespace Mladim.Client.Components.Organizations;

public partial class OrganizationStatisticsTab
{

    [Inject]
    public IOrganizationService OrganizationService { get; set; } = default!;

    [Inject]
    public IActivityService ActivityService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [CascadingParameter]
    public OrganizationVM? SelectedOrganization { get; set; }


    private OrganizationStatisticVM? organizationStatistics;   

    private IEnumerable<int> availableYears = new List<int>();
    private int selectedYear = DateTime.UtcNow.Year;

    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedOrganization != null && !availableYears.Any())
        {
            this.availableYears = this.AvailableYears();
            this.organizationStatistics = await OrganizationStatisticsAsync(selectedYear);
            this.activities = await UpcommingActivitiesAsync(5);
        }
    }

    public async Task<List<ActivityForGantt>> UpcommingActivitiesAsync(int numOfUpcommingActivities)
    {
        // If upcomming activities is equal ti null all activities will be fetched!
        var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization!.Id, numOfUpcommingActivities);

        return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange)).ToList();
    }

    private IEnumerable<int> AvailableYears()
    {
        int createdYear = SelectedOrganization!.Attributes.CreatedStamp.Year;
        return Enumerable.Range(createdYear, DateTime.UtcNow.Year - createdYear + 1).ToList();
    }

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
       this.Navigation.NavigateTo($"/activity/{args.Data.ActivityId}");


    private async Task OnYearChangedAsync(int selectedYear)
    {
        if (this.selectedYear != selectedYear)
        {
            this.selectedYear = selectedYear;
            this.organizationStatistics = await OrganizationStatisticsAsync(selectedYear);
        }

    }

    public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(int year)
    {
        return await this.OrganizationService.GetStatisticsByYearAsync(SelectedOrganization!.Id, year);
    }

    public void RowDataBound(RowDataBoundEventArgs<ActivityForGantt> args)
    {
        args.Row.AddClass(new string[] { "custom-row" });
    }
}