using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Mladim.Client;
using Mladim.Client.Shared;
using Mladim.Client.Services.Authentication;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Layouts;
using Mladim.Client.Extensions;
using Mladim.Client.Components.Organizations;
using Blazored.TextEditor;
using MudBlazor;
using Syncfusion.Blazor;
using Syncfusion.Blazor.RichTextEditor;
using Syncfusion.Blazor.Gantt;
using Mladim.Client.Models;
using Mladim.Client.Services.SubjectServices.Contracts;
using Syncfusion.Blazor.Schedule.Internal;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;

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