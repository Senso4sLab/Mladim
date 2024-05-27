using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Services.Authentication;
using Mladim.Client.Models;
using Mladim.Client.Components.Organizations;

namespace Mladim.Client.Pages;

public partial class Dashboard
{

    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    protected IOrganizationService OrganizationService { get; set; } = default!;


  

    bool hasActiveProjects = false;
    bool hasPastProjects = false;

    private bool IsOrganizationStatisticsVisible  = true;     

    public DefaultOrganization? SelectedOrganization { get; set; }

    protected async override Task OnInitializedAsync()
    {
        
        SelectedOrganization = await OrganizationService.DefaultOrganizationAsync();

        if (SelectedOrganization == null)
            return;

        IsOrganizationStatisticsVisible = !await AuthService.IsUserPolicySatisfied(SelectedOrganization!.Id.ToString(), "HasWorkerClaim");              

        var projects = await ProjectsByOrganizationAsync(SelectedOrganization.Id);

        var dateTime = DateTime.UtcNow;

        hasActiveProjects = projects.Where(p => !p.IsCompleted(dateTime)).Any();
        hasPastProjects = projects.Where(p => p.IsCompleted(dateTime)).Any();      

    }

    public async Task<IEnumerable<ProjectVM>> ProjectsByOrganizationAsync(int organizationId)
    {
        return await this.ProjectService.GetByOrganizationIdAsync(organizationId);
    }
}