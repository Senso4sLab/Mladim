using Microsoft.AspNetCore.Components;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using Mladim.Client.ViewModels.AttachedFile;
using Microsoft.JSInterop;
using System.IO;
using Mladim.Client.Services.FileService;
using Mladim.Client.MappingProfiles.Profiles.Projects;

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

    [Inject]
    public IFileService FileService { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public int OrganizationId { get; set; }    

    [Parameter]
    public int? ProjectId { get; set; }
    

    private IEnumerable<NamedEntityVM> Staff = new List<NamedEntityVM>();    
    private List<NamedEntityVM> Partners = new List<NamedEntityVM>();
     
    public bool editable = false;
   
    private ProjectVM project = new ProjectVM();
    private bool UpdateState => ProjectId != null;

    
    private long maxFileSize = 1024 * 1024 * 3;
    private int maxAllowedFiles = 5;


    protected async override Task OnInitializedAsync()
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

    public async Task DeleteProjectAsync(ProjectVM project)
    {
        var dialogResponse = await this.PopupService.ShowSimpleTextDialogAsync("Odstranitev projekta", "Ali želite odstraniti projekt?");

        if (!dialogResponse)
            return;

        if (await this.ProjectService.RemoveAsync(project.Id))
        {          
            this.PopupService.ShowSnackbarSuccess("Projekt je bil uspešno odstranjen");
        }
        else
            this.PopupService.ShowSnackbarError();

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


    private async void UploadFilesToProject(IEnumerable<IBrowserFile> files)
    {    

        foreach(var file in files)
        {
            if(file.Size > maxFileSize)
            {
                this.PopupService.ShowSnackbarError("Velikost dokumenta je omejena na 3MB");
                continue;
            }

            string fileName = Path.GetFileName(file.Name);

            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);           

            project.Files.Add(AttachedFileVM.Create(fileName, buffer.ToList(), file.ContentType));           
        }

        this.StateHasChanged();        
    }

    private void DeleteAttachedFileAsync(AttachedFileVM file)
    {
        project.Files.Remove(file);
        this.StateHasChanged();      
    }


    private async Task SelectedFileAsync(AttachedFileVM file)
    {      
        var fileStream = await this.FileService.GetFileStreamByProjectIdAsync(file.FileName, ProjectId!.Value);

        if (fileStream != null)
        {
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await JS.InvokeVoidAsync("downloadFileFromStream", file.FileName, streamRef);
        }
        else        
            this.PopupService.ShowSnackbarError();       
        
    }



    


}



