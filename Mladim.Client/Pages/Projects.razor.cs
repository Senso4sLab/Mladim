using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Models;

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
        
        if (defaultOrg != null)
            this.projects = await GetProjects();
    }

    private async Task<List<ProjectVM>> GetProjects()
    {
        var projects = await this.ProjectService.GetByOrganizationIdAsync(defaultOrg!.Id); 
        return projects.ToList();
    }



    public void AddNewProjectAsync()
    {
        this.Navigation.NavigateTo($"/organization/{defaultOrg!.Id}/project");
    }    
    public void ShowProjectAsync(ProjectVM project)
    {
        this.Navigation.NavigateTo($"/organization/{defaultOrg!.Id}/project/{project.Id}");
    }

    public async Task DeleteProjectAsync(ProjectVM project)
    {
        var dialogResponse = await this.PopupService.ShowSimpleTextDialogAsync("Odstranitev projekta", "Ali �elite odstraniti projekt?");

        if (!dialogResponse)
            return;          

        if (await this.ProjectService.RemoveAsync(project.Id))
        {
            projects.Remove(project);
            this.PopupService.ShowSnackbarSuccess("Projekt je bil uspe�no odstranjen");
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
        this.Navigation.NavigateTo($"/activities/{project.Attributes.Name}");
    }



}