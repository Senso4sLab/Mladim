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
using Mladim.Domain.Dtos;
using Mladim.Client.Services.SubjectServices.Contracts;

namespace Mladim.Client.Pages;

public partial class Projects
{
    [Inject]
    public IOrganizationService OrganizationService { get; set; }

    [Inject]
    public IProjectService ProjectService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    private DefaultOrganization? defaultOrg;

    private List<ProjectVM> projects = new List<ProjectVM>();

    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();
        
        if (defaultOrg == null)
            return;

        var projects = await this.ProjectService.GetByOrganizationIdAsync(defaultOrg.Id);
        this.projects = new List<ProjectVM>(projects);
    }


    public void AddNewProjectAsync()
    {
        this.Navigation.NavigateTo($"/organization/{defaultOrg.Id}/project");
    }

    public void UpdateProjectAsync(ProjectVM project)
    {
        this.Navigation.NavigateTo($"/organization/{defaultOrg!.Id}/project/{project.Id}");
    }

    public async Task DeleteProjectAsync(ProjectVM project)
    {
        var dialogResponse = await this.PopupService.ShowSimpleTextDialogAsync("Odstranitev projekta", "Ali želite odstraniti projekt?");

        if (!dialogResponse)
            return;          

        if (await this.ProjectService.RemoveAsync(project.Id))
        {
            projects.Remove(project);
            this.PopupService.ShowSnackbarSuccess("Projekt je bil uspešno odstranjen");
        }
        else
            this.PopupService.ShowSnackbarError();
    }


    public void AddActivityAsync(ProjectVM project)
    {
        this.Navigation.NavigateTo($"/project/{project.Id}/activity");
    }

    public void GetAllActivitiesAsync(ProjectVM project)
    {
        this.Navigation.NavigateTo($"/activities/{project.Name}");
    }



}