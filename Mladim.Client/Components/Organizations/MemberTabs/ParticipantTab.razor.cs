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

using Blazored.TextEditor;
using MudBlazor;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Domain.Dtos;
using MudBlazor.Charts;
using System.Diagnostics.Metrics;

namespace Mladim.Client.Components.Organizations.MemberTabs;

public partial class ParticipantTab
{
    [Inject]
    public IParticipantService ParticipantService { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Parameter]
    public OrganizationVM Organization { get; set; }


    private IList<ParticipantVM> Participants = new List<ParticipantVM>();

    private bool IsActive = true;

    protected async override Task OnParametersSetAsync() =>
       this.Participants = new List<ParticipantVM>(await GetParticipantsByOrganizationId());
    
    private Task<IEnumerable<ParticipantVM>> GetParticipantsByOrganizationId() =>
        this.ParticipantService.GetByOrganizationIdAsync(this.Organization.Id, this.IsActive);


    private async Task AddParticipantAsync()
    {
        var participant = new ParticipantVM();

        var dialogResponse = await this.PopupService.ShowParticipantDialog("Nov udeleženec", participant);

        if (!dialogResponse)
            return;

        participant = await this.ParticipantService.AddAsync(this.Organization.Id, participant);

        if (participant != null)
        {
            Participants.Add(participant);
            this.PopupService.ShowSnackbarSuccess("Udeleženec uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();
    }

    private async Task CheckedChangedAsync(bool isActive)
    {
        this.IsActive = isActive;
        await OnParametersSetAsync();
    }

    private async Task UpdateParticipantAsync(ParticipantVM participant)
    {

        var dialogResponse = await this.PopupService.ShowParticipantDialog("Uredi udeleženca", participant);

        if (!dialogResponse)
            return;

        var succeedResponse = await this.ParticipantService.UpdateAsync(participant);

        if (succeedResponse)
        {
            this.PopupService.ShowSnackbarSuccess("Podatki uspešno posodobljeni");
            await OnParametersSetAsync();
        }
        else
            this.PopupService.ShowSnackbarError();

    }

    private string RowStyleFunc(ParticipantVM participant, int index)
    {
        string rowCss = "font-size: 0.8rem; font-family:poppins; font-weight:400; line-height:1.0; letter-spacing:-0.024rem; color:#6e7191;";

        return index % 2 == 0 ? rowCss + "background-color:white;" : rowCss +"background-color:#EFEFEF;";
    }



}

    
