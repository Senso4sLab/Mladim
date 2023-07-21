using Microsoft.AspNetCore.Components;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Microsoft.AspNetCore.Components.Forms;

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
    

    private IEnumerable<NamedEntityVM> Staff = new List<NamedEntityVM>();    
    private List<NamedEntityVM> Partners = new List<NamedEntityVM>();
     
    public bool editable = false;
    private TextEditor? textEditor;  
    private ProjectVM project = new ProjectVM();
    IList<IBrowserFile> AttachedFiles = new List<IBrowserFile>();
    private bool UpdateState => ProjectId != null;



    protected async override Task OnParametersSetAsync()
    {
        this.Staff = new List<NamedEntityVM>(await StaffMembersByOrganizationIdAsync());
        this.Partners = new List<NamedEntityVM>(await PartnersByOrganizationIdAsync());

        if (UpdateState)
            await this.FetchingMembersForProjectUpdate();
        else
            editable = true;
    }


    public async Task OnProjectEditableChanged(bool toggled)
    {
        editable = toggled;              
        if(!toggled)        
            await SaveProjectAsync();        
    }
    

    private Task<IEnumerable<NamedEntityVM>> StaffMembersByOrganizationIdAsync() =>
        this.StaffService.GetBaseByOrganizationIdAsync(OrganizationId, true);


    private Task<IEnumerable<NamedEntityVM>> PartnersByOrganizationIdAsync() =>
        this.PartnerService.GetBaseByOrganizationIdAsync(OrganizationId, true);

    private async Task FetchingMembersForProjectUpdate()
    {
       
        var projectResponse = await this.ProjectService.GetByProjectIdAsync(ProjectId!.Value);

        if (projectResponse != null)
            project = projectResponse;
    }

  

    public async Task SaveProjectAsync()
    {
        await textEditor!.LoadHtmlText();
                
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

            if (httpResponse)
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


    private void UploadFilesToProject(IEnumerable<IBrowserFile> files)
    {
        files.ToList().ForEach(file => this.AttachedFiles.Add(file));  
      
    }

    private Task DeleteAttachedFileAsync(IBrowserFile file)
    {

        this.AttachedFiles.Remove(file);

        return Task.CompletedTask;
    }


    private Task SelectedFileAsync(IBrowserFile file)
    {
        return Task.CompletedTask;
    }



    


}



