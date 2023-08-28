using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
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
        var upcommingActivities = await this.ActivityService.GetByOrganizationIdAsync(SelectedOrganization!.Id, numOfUpcommingActivities);

        return upcommingActivities.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange))
         .ToList();
    }

    private IEnumerable<int> AvailableYears()
    {
        int createdYear = SelectedOrganization!.Attributes.CreatedStamp.Year;
        return Enumerable.Range(createdYear, DateTime.UtcNow.Year - createdYear + 1).ToList();
    }

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
       this.Navigation.NavigateTo($"/activity/{args.Data.Id}");


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
}