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
using System.Globalization;
using Mladim.Client.ViewModels.Activity;
using Syncfusion.Blazor.Grids;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.ViewModels.Project;
using System.Runtime.InteropServices;
using Mladim.Client.ViewModels.Organization;
using Mladim.Domain.Models;
using System.Runtime.CompilerServices;
using Mladim.Client.Models;

namespace Mladim.Client.Components.Organizations;

public partial class ProjectStatisticsTab
{
    [Inject]
    public IActivityService ActivityService { get; set; } = default!;


    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;    

    [Parameter]
    public IEnumerable<ProjectVM> Projects { get; set; } = new List<ProjectVM>();

    [Parameter]
    public bool PastActivities { get; set; }

    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private ProjectVM? selectedProject;

    private ProjectStatisticsVM? projectStatistics;

    //private List<DoughnutPiece> GenderDoughnut = new List<DoughnutPiece>();
    //private List<DoughnutPiece> AgeDoughnut = new List<DoughnutPiece>();

    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>
      this.Navigation.NavigateTo($"/activity/{args.Data.Id}");
       

    protected override async Task  OnInitializedAsync()
    {
        selectedProject = Projects.FirstOrDefault();

        await OnChangedSelectedProjectAsync(selectedProject.Id);
    }

    private async Task OnChangedSelectedProjectAsync(int projectId)
    {
        projectStatistics = await ProjectStatisticsAsync(projectId);
        activities = await ActivitiesAsync(projectId);
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


    private async Task OnProjectChangedAsync(ProjectVM project)
    {
        if (selectedProject.Id != project.Id)
        {
            selectedProject = project;
            await OnChangedSelectedProjectAsync(selectedProject.Id);
        }
    }

}