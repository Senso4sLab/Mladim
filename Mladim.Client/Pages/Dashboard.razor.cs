using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using System.ComponentModel;

namespace Mladim.Client.Pages;

public partial class Dashboard
{   

    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    //[Inject]
    //public IActivityService ActivityService { get; set; } = default!;

    //[Inject]
    //public NavigationManager Navigation { get; set; } = default!;

    [CascadingParameter]
    public OrganizationVM? SelectedOrganization { get; set; }

    //private int selectedYear = DateTime.UtcNow.Year;

    //private IEnumerable<int> availableYears = new List<int>();

    //private OrganizationStatisticVM? organizationStatistics;   

    //private List<ActivityForGantt> activities = new List<ActivityForGantt>();

   

    private IEnumerable<ProjectVM> activeProjects = new List<ProjectVM>();  
    private IEnumerable<ProjectVM> pastProjects = new List<ProjectVM>();

    

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedOrganization is OrganizationVM organization)
        {
            var projects = await ProjectsByOrganizationAsync(organization.Id);

            var dateTime = DateTime.UtcNow;

            activeProjects = projects.Where(p => !p.IsCompleted(dateTime)).ToList();
            pastProjects = projects.Where(p => p.IsCompleted(dateTime)).ToList();
        }
    }

    public async Task<IEnumerable<ProjectVM>> ProjectsByOrganizationAsync(int organizationId) =>
        await this.ProjectService.GetByOrganizationIdAsync(organizationId);    
    


    //public async Task<OrganizationStatisticVM?> OrganizationStatisticsAsync(int year)
    //{
    //    return await this.OrganizationService.GetStatisticsByYearAsync(SelectedOrganization!.Id, year);
    //}

    //public async Task<List<ActivityForGantt>> UpcommingActivitiesAsync(int numOfUpcommingActivities)
    //{
    //   var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization!.Id, numOfUpcommingActivities);

    //   return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange))
    //    .ToList();
    //}

    //private IEnumerable<int> AvailableYears()
    //{
    //    int createdYear = SelectedOrganization!.Attributes.CreatedStamp.Year;
    //    return Enumerable.Range(createdYear, DateTime.UtcNow.Year - createdYear + 1).ToList();
    //}

    //public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
    //   this.Navigation.NavigateTo($"/activity/{args.Data.Id}");


    //private async Task OnYearChangedAsync(int selectedYear)
    //{
    //    if(this.selectedYear != selectedYear)
    //    {
    //        this.selectedYear = selectedYear;
    //        this.organizationStatistics = await OrganizationStatisticsAsync(selectedYear);
    //    }       

    //}
}