using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using MudBlazor;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Models;
using Mladim.Client.ViewModels.Members.StaffMembers;
using Syncfusion.Blazor.Grids;
using Mladim.Client.ViewModels.Activity;

namespace Mladim.Client.Pages;

public partial class Activities
{
    [Inject]
    public IActivityService ActivityService { get; set; } 

    [Inject]
    public IStaffMemberService StaffMemberService { get; set; }

    [Inject]
    public IOrganizationService OrganizationService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }    

    [Parameter]
    public int? ProjectId { get; set; }

    private List<ActivityForGantt> activities = new List<ActivityForGantt>();

    private List<ActivityForGantt> filteredActivities { get; set; } = new List<ActivityForGantt>();
    private IEnumerable<StaffMemberLeadVM> leadStaff { get; set; } = new List<StaffMemberLeadVM>();

    private DefaultOrganization? defaultOrg;   

    private DateRange dateRange = new DateRange();

    private List<NamedEntityVM> projects = new List<NamedEntityVM>();
    private List<NamedEntityVM> selectedProjects = new List<NamedEntityVM>();


    private List<NamedEntityVM> projectLeads = new List<NamedEntityVM>();
    private NamedEntityVM? projectLead;


    private List<NamedEntityVM> activityLeads = new List<NamedEntityVM>();
    private NamedEntityVM? activityLead;

    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        if (defaultOrg != null)
        {
            await GetActivitiesAsync(defaultOrg);
            await GetLeadStaffAsync(defaultOrg);
            FindSelectedProject();
            await ApplyActivitiesFilterAsync();
        }        
    }


   

    private void FindSelectedProject()
    {
        if (this.ProjectId is int id && this.projects.FirstOrDefault(p => p.Id == id) is NamedEntityVM project)
            this.selectedProjects.Add(project);
        else
            this.ProjectId = null;
    }      
    



    private Task ApplyActivitiesFilterAsync() =>
        Task.Run(() =>
        {
            IEnumerable<ActivityForGantt> gattActivities = activities;

            if (ProjectId is int projectId)
                gattActivities = gattActivities.Where(a => a.ProjectId == projectId);

            if (dateRange.Start is DateTime start && dateRange.End is DateTime end)
                gattActivities = gattActivities.Where(a => a.StartDate >= start && a.EndDate <= end);

            if (projectLead is NamedEntityVM pl && this.leadStaff.FirstOrDefault(lsm => lsm.Id == pl.Id) is StaffMemberLeadVM psml)
                gattActivities = gattActivities.Where(a => psml.ProjectIds.Any(id => id == a.ProjectId));

            if (activityLead is NamedEntityVM al && this.leadStaff.FirstOrDefault(lsm => lsm.Id == al.Id) is StaffMemberLeadVM asml)
                gattActivities = gattActivities.Where(a => asml.ActivityIds.Any(id => id == a.ActivityId));

            filteredActivities = gattActivities.ToList();
        });    

    private async Task GetActivitiesAsync(DefaultOrganization defaultOrg)
    {        
        var activityProjects = await this.ActivityService.GetByOrganizationIdAsync(defaultOrg.Id);
        
        projects = activityProjects.Select(a => a.Project).Distinct()
            .ToList();

        activities = activityProjects.Select((a, i) => ActivityForGantt.Create(i + 1, a.Id, a.Attributes.Name, a.Project, a.TimeRange))
            .ToList();      
    }

    private async Task GetLeadStaffAsync(DefaultOrganization defaultOrg)
    {
        leadStaff = await this.StaffMemberService.GetLeadStaffMembersAsync(defaultOrg.Id);

        projectLeads = leadStaff.Where(sml => sml.ProjectIds.Any())
            .Select(sml => NamedEntityVM.Create(sml.Id, sml.FullName))
            .ToList();

        activityLeads = leadStaff.Where(sml => sml.ActivityIds.Any())
          .Select(sml => NamedEntityVM.Create(sml.Id, sml.FullName))
          .ToList();       
    }


    public void SelectedActivity(RowSelectEventArgs<ActivityForGantt> args) =>    
        this.Navigation.NavigateTo($"/activity/{args.Data.Id}");
      

    private async Task OnDateRangeChanged(DateRange dateRange)
    {
        this.dateRange = dateRange;
        await ApplyActivitiesFilterAsync();        
    }

    private async Task OnProjectNameChanged(IEnumerable <NamedEntityVM> project)
    {
        this.ProjectId = project.FirstOrDefault()?.Id;
        selectedProjects = project.ToList();
        await ApplyActivitiesFilterAsync();      
    }

    private async Task OnProjectLeaderChanged(IEnumerable<NamedEntityVM> projectLeads)
    {
        projectLead = projectLeads.FirstOrDefault();
        await ApplyActivitiesFilterAsync();        
    }

    private async Task OnActivityLeaderChanged(IEnumerable<NamedEntityVM> activityLeads)
    {
        activityLead = activityLeads.FirstOrDefault();
        await ApplyActivitiesFilterAsync();       
    }


}




