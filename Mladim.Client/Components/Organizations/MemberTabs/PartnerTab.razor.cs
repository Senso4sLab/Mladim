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
using Mladim.Client.Services.SubjectServices.Contracts;

namespace Mladim.Client.Components.Organizations.MemberTabs;

public partial class PartnerTab
{
    [Inject]
    public IPartnerService PartnerService { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Parameter]
    public OrganizationVM Organization { get; set; }


    private IList<PartnerVM> Partners = new List<PartnerVM>();

    private bool IsActive = true;

    protected async override Task OnParametersSetAsync() =>
       this.Partners = new List<PartnerVM>(await GetPartnersByOrganizationId());


    private Task<IEnumerable<PartnerVM>> GetPartnersByOrganizationId() =>
        this.PartnerService.GetByOrganizationIdAsync(this.Organization.Id, this.IsActive);


    private async Task AddPartnerAsync()
    {
        var partner = new PartnerVM();

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Nov partner", partner);

        if (!dialogResponse)
            return;

        partner = await this.PartnerService.AddAsync(this.Organization.Id, partner);

        if (partner != null)
        {
            Partners.Add(partner);
            this.PopupService.ShowSnackbarSuccess("Partner uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();
    }

    private async Task CheckedChangedAsync(bool isActive)
    {
        this.IsActive = isActive;
        await OnParametersSetAsync();
    }

    private async Task UpdatePartnerAsync(PartnerVM partner)
    {

        var dialogResponse = await this.PopupService.ShowPartnerDialog("Uredi partnerja", partner);

        if (!dialogResponse)
            return;

        var succeedResponse = await this.PartnerService.UpdateAsync(partner);

        if (succeedResponse)
        {
            this.PopupService.ShowSnackbarSuccess("Podatki uspešno posodobljeni");
            await OnParametersSetAsync();
        }
        else
            this.PopupService.ShowSnackbarError();

    }
}