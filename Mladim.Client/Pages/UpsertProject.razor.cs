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
using Mladim.Domain.Models;

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


    private List<PartnerVM> partners = new List<PartnerVM>();

    private IEnumerable<BaseMemberVM> staff = new List<BaseMemberVM>();
    private IEnumerable<BaseMemberVM> staffleads = new List<BaseMemberVM>();
    private IEnumerable<BaseMemberVM> administrators = new List<BaseMemberVM>();
    private IEnumerable<PartnerVM> selectedPartners = new List<PartnerVM>();

    private TextEditor? textEditor;
    private DateRange projectDuration;

    private ProjectVM project = new ProjectVM();

    private bool UpdateState => ProjectId != null;
    protected async override Task OnParametersSetAsync()
    {
        if (UpdateState)        
           await this.FetchingMembersForProjectUpdate();
             
        
        projectDuration = new DateRange(project.Start, project.Start);        

    }


    private async Task FetchingMembersForProjectUpdate()
    {
        staff = new List<BaseMemberVM>(await StaffMembersByOrganizationIdAsync());
        //partners = await GetActivePartnersAsync();
        var projectResponse = await this.ProjectService.GetByProjectIdAsync(ProjectId.Value);

        if (projectResponse == null)
            return;

        administrators = projectResponse.Staff.Where(sm => !sm.IsLead).Select(sm => new BaseMemberVM { Id = sm.StaffMemberId, Name = sm.Name}).ToList();    
        staffleads = projectResponse.Staff.Where(sm => sm.IsLead).Select(sm => new BaseMemberVM { Id = sm.StaffMemberId, Name = sm.Name }).ToList();       
        //selectedPartners = this.partners.Where(p => project.Partners.Any(pa => pa.Id == p.Id)).ToList();
        
    }

    private Task<IEnumerable<BaseMemberVM>> StaffMembersByOrganizationIdAsync() =>
      this.StaffService.GetBaseByOrganizationIdAsync(OrganizationId, true);
       

   




    public async Task SaveProjectAsync()
    {
        await textEditor!.LoadHtmlText();

        project.Start = projectDuration.Start!.Value;
        project.End = projectDuration.End!.Value;        
        
        project.Partners = selectedPartners.ToList();
        project.Staff = staffleads.Select(sm => new StaffMemberProjectVM { IsLead = true, StaffMemberId = sm.Id.Value }).ToList();
        project.Staff.AddRange(administrators.Select(sm => new StaffMemberProjectVM { StaffMemberId = sm.Id.Value }).ToList());


        await this.ProjectService.UpdateAsync(project);
        
        if (UpdateState)
        {
            var httpResponse = await this.ProjectService.UpdateAsync(project);

            if (httpResponse)
                this.PopupService.ShowSnackbarSuccess("Projekt je uspešno posodobljen");
            else
                this.PopupService.ShowSnackbarError();
        }
        else
        {
        
            var httpResponse = await this.ProjectService.AddAsync(project, OrganizationId);

            if (httpResponse != null)
                this.PopupService.ShowSnackbarSuccess("Projekt je uspešno dodan");
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

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Dodajanje partnerja", partner);

        if (!dialogResponse)
            return;

        partner = await this.PartnerService.AddAsync(this.OrganizationId, partner);

        if (partner != null)
        {
            partners.Add(partner);
            this.PopupService.ShowSnackbarSuccess("Partner je uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();
    }   
}



