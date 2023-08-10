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
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Models;
using Mladim.Client.Models;

namespace Mladim.Client.Pages;

public partial class Activities
{
    [Inject]
    public IActivityService ActivityService { get; set; }

    [Inject]
    public IOrganizationService OrganizationService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    

    [Parameter]
    public string? ProjectName { get; set; }
                 
    private List<ActivityWithProjectNameVM> activities { get; set; } = new List<ActivityWithProjectNameVM>();

    private DefaultOrganization defaultOrg;

    private List<TaskData> TaskCollection { get; set; }
    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        if (defaultOrg != null)
           activities = await GetActivities(defaultOrg);



        this.TaskCollection = GetTaskCollection();
    }

    private async Task<List<ActivityWithProjectNameVM>> GetActivities(DefaultOrganization defaultOrg)
    {        
        var activityProjects = await this.ActivityService.GetByOrganizationIdAsync(defaultOrg.Id);
        return activityProjects.ToList();
    }

    public void ShowActivityAsync(ActivityWithProjectNameVM activity)
    {
        this.Navigation.NavigateTo($"/activity/{activity.Id}");
    }

    public static List<TaskData> GetTaskCollection()
    {
        List<TaskData> Tasks = new List<TaskData>() {
            new TaskData() { TaskId = 1, TaskName = "Project initiation", StartDate = new DateTime(2022, 04, 05), EndDate = new DateTime(2022, 04, 21), },
            new TaskData() { TaskId = 2, TaskName = "Identify Site location", StartDate = new DateTime(2022, 04, 05), Duration = "4", Progress = 50, ParentId = 1 },
            new TaskData() { TaskId = 3, TaskName = "Perform soil test", StartDate = new DateTime(2022, 04, 05), Duration = "4", Progress = 50, ParentId = 1 }
        };
        return Tasks;
    }




    private Func<ActivityWithProjectNameVM, bool> _quickFilter => x =>
    {
        if (string.IsNullOrWhiteSpace(ProjectName))
            return true;

        if (x.ProjectName.Contains(ProjectName, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };


}


public class TaskData
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Duration { get; set; }
    public int Progress { get; set; }
    public int? ParentId { get; set; }
}