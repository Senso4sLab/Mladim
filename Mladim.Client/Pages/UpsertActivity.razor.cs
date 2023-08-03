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
using Mladim.Domain.Enums;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Models;
using Mladim.Client.Services.SubjectServices.Implementations;
using Mladim.Domain.Models;
using Mladim.Client.Services.FileService;
using Mladim.Client.ViewModels.AttachedFile;

namespace Mladim.Client.Pages;

public partial class UpsertActivity
{
    [Inject]
    public IActivityService ActivityService { get; set; }

    [Inject]
    public IOrganizationService OrganizationService { get; set; }

    [Inject]
    public IStaffMemberService StaffMemberService { get; set; }
    [Inject]
    public IParticipantService ParticipantService { get; set; }
    [Inject]
    public IPartnerService PartnerService { get; set; }

    [Inject]
    public IGroupService GroupService { get; set; }

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Inject]
    public IFileService FileService { get; set; } = default!;

    [Inject]
    public IJSRuntime JS { get; set; } = default!;

    [Parameter]
    public int? ActivityId { get; set; }

    [Parameter]
    public int? ProjectId { get; set; }

    

    private ActivityVM activity = new ActivityVM(); 
    
    private TextEditor? textEditor;
    private bool UpdateState =>
        ActivityId != null;
    private int TotalAnonymousParticipants =>
        activity.AnonymousParticipantActivities?.Sum(ap => ap.Number) ?? 0;

    private DefaultOrganization defaultOrg = default!;

    private List<NamedEntityVM> staff = new List<NamedEntityVM>();
    private List<NamedEntityVM> partners = new List<NamedEntityVM>();
    private List<NamedEntityVM> participants = new List<NamedEntityVM>();
    private List<NamedEntityVM> participantGroups = new List<NamedEntityVM>();

    public bool editable = false;

    private long maxFileSize = 1024 * 1024 * 3;
    private int maxAllowedFiles = 5;

    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        if (defaultOrg == null)
            return;

        staff = new List<NamedEntityVM>(await StaffMemberService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        partners = new List<NamedEntityVM>(await PartnerService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        participants = new List<NamedEntityVM>(await ParticipantService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        participantGroups = new List<NamedEntityVM>(await GroupService.GetByOrganizationIdAsync(defaultOrg.Id, GroupType.Activity, true));

        if (UpdateState)
            activity = await ActivityService.GetByActivityIdAsync(ActivityId.Value);
        else
            editable = true;
    }


   

    public async Task OnActivityEditableChanged(bool toggled)
    {
        editable = toggled;
        if (!toggled)
            await SaveActivityAsync();
    }



    public async Task SaveActivityAsync()
    {
        await textEditor!.GetHTMLTextAsync();
        
        if (UpdateState)
        {           
            var httpResponse = await ActivityService.UpdateAsync(activity);

            if (httpResponse)
                this.PopupService.ShowSnackbarSuccess("Aktivnost uspešno posodobljena");
            else
                this.PopupService.ShowSnackbarError();
        }
        else
        {
            var httpResponse = await ActivityService.AddAsync(activity, ProjectId!.Value);

            if (httpResponse)
                this.PopupService.ShowSnackbarSuccess("Aktivnost uspešno dodana");
            else
                this.PopupService.ShowSnackbarError();
        }

        Navigation.NavigateTo("/activities");
    }

    public void CancelProjectAsync()
    {
        Navigation.NavigateTo("/activities");
    }

    public async Task AddPartnerAsync()
    {
        var partner = new PartnerVM();

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Nov partner", partner);

        if (!dialogResponse)
            return;


        var partnerResult = await this.PartnerService.AddAsync(defaultOrg.Id, partner);
               

        if (partnerResult != null)
        {
            this.partners.Add(partnerResult);
            this.PopupService.ShowSnackbarSuccess("Partner uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();

    }

    public async Task AddParticipantAsync()
    {
        var participant = new ParticipantVM();

        var dialogResponse = await this.PopupService.ShowParticipantDialog("Nov udeleženec", participant);

        if (!dialogResponse)
            return;

        var participantResult = await this.ParticipantService.AddAsync(defaultOrg.Id, participant);

        if (participantResult != null)
        {
            participants.Add(participantResult);
            this.PopupService.ShowSnackbarSuccess("Udeleženec uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();

    }    

    public async Task AddAnonymousParticipantAsync()
    {
        var resultGroups = await this.PopupService
            .ShowAnonymousParticipantGroupsDialog("Dodajanje udeležencev po starostnih skupinah in spolu", activity.AnonymousParticipantActivities);
        
        if(resultGroups.Any())
        {
            this.activity.AnonymousParticipantActivities = resultGroups.ToList();
            this.StateHasChanged();
        }       
    }


    private async void UploadFilesToProject(IEnumerable<IBrowserFile> files)
    {
        foreach (var file in files)
        {
            if (file.Size > maxFileSize)
            {
                this.PopupService.ShowSnackbarError("Velikost dokumenta je omejena na 3MB");
                continue;
            }

            string fileName = Path.GetFileName(file.Name);

            var buffer = new byte[file.Size];
            await file.OpenReadStream().ReadAsync(buffer);

            activity.Files.Add(AttachedFileVM.Create(fileName, buffer.ToList(), file.ContentType));
        }

        this.StateHasChanged();
    }

    private void DeleteAttachedFileAsync(AttachedFileVM file)
    {
        activity.Files.Remove(file);
        this.StateHasChanged();
    }


    private async Task SelectedFileAsync(AttachedFileVM file)
    {
        var fileStream = await this.FileService.GetFileStreamByActivityIdAsync(file.FileName, ActivityId!.Value);

        if (fileStream != null)
        {
            using var streamRef = new DotNetStreamReference(stream: fileStream);
            await JS.InvokeVoidAsync("downloadFileFromStream", file.FileName, streamRef);
        }
        else
            this.PopupService.ShowSnackbarError();

    }

}