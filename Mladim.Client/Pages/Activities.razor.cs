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
                 
    private List<ActivityWithProjectNameVM> activities = new List<ActivityWithProjectNameVM>();

    private DefaultOrganization defaultOrg;
    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        if (defaultOrg != null)
           activities = await GetActivities(defaultOrg);        
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

    public async Task DeleteActivityAsync(ActivityWithProjectNameVM activity)
    {

        var dialogResponse = await this.PopupService.ShowSimpleTextDialogAsync("Odstranjevanje aktivnosti", "Ali želite odstraniti aktivnost?");

        if (!dialogResponse)
            return;

        var htmlResponse = await this.ActivityService.RemoveAsync(activity.Id);

        if (htmlResponse)
        {
            activities.Remove(activity);
            this.PopupService.ShowSnackbarSuccess("Aktivnost je bila uspešno odstranjena");
        }
        else
            this.PopupService.ShowSnackbarError();
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