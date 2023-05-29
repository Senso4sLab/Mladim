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
    protected NavigationManager Navigation { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Parameter]
    public int? ActivityId { get; set; }

    [Parameter]
    public int? ProjectId { get; set; }

    

    private ActivityVM activity = new ActivityVM(); 


    
    private TextEditor? textEditor;
    private bool UpdateState => ActivityId != null;

    private string anonymousParticipantsText = string.Empty;  
    
    
    private DefaultOrganization defaultOrg;

    private IEnumerable<MemberBaseVM> staff = new List<MemberBaseVM>();
    private List<MemberBaseVM> partners = new List<MemberBaseVM>();
    private List<MemberBaseVM> participants = new List<MemberBaseVM>();
    private List<AnonymousParticipantsVM> anonymousParticipantGroups = new List<AnonymousParticipantsVM>();




    protected async override Task OnInitializedAsync()
    {
        defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        if (defaultOrg == null)
            return;

        staff = new List<MemberBaseVM>(await StaffMemberService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        partners = new List<MemberBaseVM>(await PartnerService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        participants = new List<MemberBaseVM>(await ParticipantService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
            return;
        
        if (UpdateState)            
            activity = await ActivityService.GetByActivityIdAsync(ActivityId.Value);            

        anonymousParticipantGroups = GetAnnonymousParticipants().ToList();
       
    }


    protected async override Task OnParametersSetAsync()
    {
        //defaultOrg = await this.OrganizationService.DefaultOrganizationAsync();

        //if (defaultOrg == null)
        //    return;
        
        //staff = new List<MemberBaseVM>(await StaffMemberService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        //partners = new List<MemberBaseVM>(await PartnerService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));
        //participants = new List<MemberBaseVM>(await ParticipantService.GetBaseByOrganizationIdAsync(defaultOrg.Id, true));           
        
        //if (UpdateState)
        //    activity = await ActivityService.GetByActivityIdAsync(ActivityId.Value);

        //anonymousParticipantGroups = GetAnnonymousParticipants().ToList();

    }

    public IEnumerable<AnonymousParticipantsVM> GetAnnonymousParticipants()
    {
        foreach (var ageGroup in Enum.GetValues<AgeGroups>())
        {
            foreach (var gender in Enum.GetValues<Gender>())
            {               
                var apgroup = new AnonymousParticipantsVM
                {
                    AgeGroup = ageGroup,
                    Gender = gender,
                    Number = 0,
                };

                var existedGroup = activity?.AnonymousParticipants.FirstOrDefault(apg => apg.Equals(apgroup));
                apgroup.Number = existedGroup != null ? existedGroup.Number : 0;
                yield return apgroup;
            }
        }
    }


    private async Task UpdateUIComponentsAsync()
    {
        //activity = await ActivityService.GetByActivityIdAsync(ActivityId.Value);   

        //if (activity == null)
        //    return;

        //selectedMembers = staff.Where(s => activity.Members.Any(pa => !pa.IsLead && pa.MemberId == s.Id)).ToList();
        //selectedLeadMembers = staff.Where(s => activity.Members.Any(pa => pa.IsLead && pa.MemberId == s.Id)).ToList();
        //selectedParticipantMembers = participants.Where(s => activity.Members.Any(pa => pa.MemberId == s.Id)).ToList();
        //selectedPartners = partners.Where(p => activity.Partners.Any(pa => pa.Id == p.Id)).ToList();
        //activityDuration = new DateRange(this.activity.Start, this.activity.End);
        //anonymousParticipantGroups = await GetAnonymousParticipantsAsync(ActivityId!.Value);
        //anonymousParticipantsText = GetAnonymousParticipantsText(anonymousParticipantGroups);
    }

    





    public async Task SaveActivityAsync()
    {
        await textEditor!.LoadHtmlText();
        //activity.Start = activityDuration.Start!.Value;
        //activity.End = activityDuration.End!.Value;
        //activity.Partners = selectedPartners.ToList();


        //var leadMembers = selectedLeadMembers.Select(s => new SubjectMemberDto { IsLead = true, MemberId = s.Id.Value });
        //var members = selectedMembers.Select(s => new SubjectMemberDto { MemberId = s.Id.Value });

        //var participantMembers = selectedParticipantMembers.Select(s => new SubjectMemberDto { MemberId = s.Id.Value });
        //activity.Members = leadMembers.Union(members).Union(participantMembers).ToList();

        activity.AnonymousParticipants = anonymousParticipantGroups;

        if (UpdateState)
        {           
            var httpResponse = await ActivityService.UpdateAsync(activity);

            if (httpResponse)
                this.PopupService.ShowSnackbarSuccess("Aktivnost je uspe�no posodobljena");
            else
                this.PopupService.ShowSnackbarError();
        }
        else
        {
            var httpResponse = await ActivityService.AddAsync(activity, ProjectId!.Value);

            if (httpResponse != null)
                this.PopupService.ShowSnackbarSuccess("Aktivnost je uspe�no dodana");
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

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Dodajanje partnerja", partner);

        if (!dialogResponse)
            return;


        var partnerResult = await this.PartnerService.AddAsync(defaultOrg.Id, partner);
               

        if (partnerResult != null)
        {
            this.partners.Add(partnerResult);
            this.PopupService.ShowSnackbarSuccess("Partner je uspe�no dodan");
        }
        else
            this.PopupService.ShowSnackbarError();

    }

    public async Task AddParticipantAsync()
    {

        var participant = new ParticipantVM();

        var dialogResponse = await this.PopupService.ShowParticipantDialog("Dodajanje udele�enca", participant);

        if (!dialogResponse)
            return;

        var participantResult = await this.ParticipantService.AddAsync(defaultOrg.Id, participant);

        if (participantResult != null)
        {
            participants.Add(participantResult);
            this.PopupService.ShowSnackbarSuccess("Udele�enec je uspe�no dodan");
        }
        else
            this.PopupService.ShowSnackbarError();

    }


    //private string GetAnonymousParticipantsText(List<AnonymousParticipantGroupDto> participantGroups)
    //{
    //    if (!participantGroups.Any(p => p.Number > 0))
    //        return string.Empty;

    //    return $"�t. mo�kih = {participantGroups.Where(x => x.Gender == Gender.Male).Sum(x => x.Number)}, �t. �ensk = {participantGroups.Where(x => x.Gender == Gender.Female).Sum(x => x.Number)}, skupaj = {participantGroups.Sum(x => x.Number)}";
    //} 

    

    public async Task AddAnonymousParticipantAsync()
    {
        var resultGroups = await this.PopupService.ShowAnonymousParticipantGroupsDialog("Dodajanje udele�encev po starostnih skupinah in spolu", anonymousParticipantGroups);
        anonymousParticipantGroups = resultGroups.ToList();
    }

}