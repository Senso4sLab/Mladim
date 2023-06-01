using Microsoft.AspNetCore.Components;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;

namespace Mladim.Client.Pages;

public partial class UpsertProject
{
    [Inject]
    public IProjectService ProjectService { get; set; }

    [Inject]
    public IPartnerService PartnerService { get; set; }

    [Inject]
    public IStaffMemberService StaffService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }


    [Parameter]
    public int OrganizationId { get; set; }

    [Parameter]
    public int? ProjectId { get; set; }


    

    private IEnumerable<MemberBaseVM> Staff = new List<MemberBaseVM>();    
    private List<MemberBaseVM> Partners = new List<MemberBaseVM>();


    private TextEditor? textEditor;  
    private ProjectVM project = new ProjectVM();

    private bool UpdateState => ProjectId != null;
    protected async override Task OnParametersSetAsync()
    {
        this.Staff = new List<MemberBaseVM>(await StaffMembersByOrganizationIdAsync());
        this.Partners = new List<MemberBaseVM>(await PartnersByOrganizationIdAsync());
        
        if (UpdateState)        
           await this.FetchingMembersForProjectUpdate();      

    }

    private Task<IEnumerable<MemberBaseVM>> StaffMembersByOrganizationIdAsync() =>
        this.StaffService.GetBaseByOrganizationIdAsync(OrganizationId, true);


    private Task<IEnumerable<MemberBaseVM>> PartnersByOrganizationIdAsync() =>
        this.PartnerService.GetBaseByOrganizationIdAsync(OrganizationId, true);

    private async Task FetchingMembersForProjectUpdate()
    {
       
        var projectResponse = await this.ProjectService.GetByProjectIdAsync(ProjectId!.Value);

        if (projectResponse == null)
            return;

        project = projectResponse;
        //LeadStaffMembers = project.LeadStaff.ToList(); 
        //Administrators   = project.Administrators.ToList();
       // projectDurationRange = new DateRange(project.Start, project.Start);
        //selectedPartners = this.partners.Where(p => project.Partners.Any(pa => pa.Id == p.Id)).ToList();

    }

  

    public async Task SaveProjectAsync()
    {
        await textEditor!.LoadHtmlText();

        //project.Start = projectDurationRange.Start!.Value;
        //project.End = projectDurationRange.End!.Value;         
        //project.Partners = Partners.ToList();        
        //project.Staff = LeadStaffMembers.Select(sm => new StaffMemberProjectVM { IsLead = true, StaffMemberId = sm.MemberId.Value }).ToList();
        //project.Staff.AddRange(Administrators.Select(sm => new StaffMemberProjectVM { StaffMemberId = sm.MemberId.Value }).ToList());

       
        
        if (UpdateState)
        {
            var httpResponse = await this.ProjectService.UpdateAsync(project);

            if (httpResponse)
                this.PopupService.ShowSnackbarSuccess("Projekt uspešno posodobljen");
            else
                this.PopupService.ShowSnackbarError();
        }
        else
        {
        
            var httpResponse = await this.ProjectService.AddAsync(project, OrganizationId);

            if (httpResponse != null)
                this.PopupService.ShowSnackbarSuccess("Projekt uspešno dodan");
            else
                this.PopupService.ShowSnackbarError();
        }

        Navigation.NavigateTo("/projects");
    }

    public void CancelProjectAsync()
    {
        Navigation.NavigateTo("/projects");
    }

    private async Task AddPartnerAsync()
    {
        var partner = new PartnerVM();

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Nov partner", partner);

        if (!dialogResponse)
            return;

        partner = await this.PartnerService.AddAsync(this.OrganizationId, partner);

        if (partner != null)
        {
            Partners.Add(partner);
            this.PopupService.ShowSnackbarSuccess("Partner uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();
    }   
}



