using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using System.ComponentModel;
using Mladim.Client.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Mladim.Client.Models;

namespace Mladim.Client.Pages;

public partial class Dashboard
{

    [Inject]
    public IProjectService ProjectService { get; set; } = default!;

    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    public DefaultOrganization? SelectedOrganization { get; set; }


    [Inject]
    protected IOrganizationService OrganizationService { get; set; } = default!;


    private IEnumerable<ProjectVM> activeProjects = new List<ProjectVM>();
    private IEnumerable<ProjectVM> pastProjects = new List<ProjectVM>();

    private bool IsOrganizationStatisticsVisible { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        
        SelectedOrganization = await OrganizationService.DefaultOrganizationAsync();

        if (SelectedOrganization == null)
            return;

        IsOrganizationStatisticsVisible = !await AuthService.IsUserPolicySatisfied(SelectedOrganization!.Id.ToString(), "HasWorkerClaim");

        if (!IsOrganizationStatisticsVisible)
            return;

        var projects = await ProjectsByOrganizationAsync(SelectedOrganization.Id);

        var dateTime = DateTime.UtcNow;

        activeProjects = projects.Where(p => !p.IsCompleted(dateTime)).ToList();
        pastProjects = projects.Where(p => p.IsCompleted(dateTime)).ToList();

    }

    public async Task<IEnumerable<ProjectVM>> ProjectsByOrganizationAsync(int organizationId)
    {
        return await this.ProjectService.GetByOrganizationIdAsync(organizationId);
    }
}