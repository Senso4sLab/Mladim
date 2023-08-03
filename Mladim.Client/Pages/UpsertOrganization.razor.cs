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
using Mladim.Client.Models;

namespace Mladim.Client.Pages;

public partial class UpsertOrganization
{
    [Inject]
    public IOrganizationService OrganizationService { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    [Parameter]
    public string? UserId { get; set; }

    [Parameter]
    public int? OrgId { get; set; }   

    private bool UpdateState => OrgId != null;

    private OrganizationVM? organization = new OrganizationVM();

    private OrganizationTab? orgDetailsTab = default!;

    protected async override Task OnInitializedAsync()
    {       
        if (this.UpdateState && OrgId != null)
            organization = await this.OrganizationService.GetByIdAsync(OrgId.Value);

    }

    private async Task UpsertOrganizationAsync()
    {
        this.organization!.Attributes.Description = await orgDetailsTab!.textEditor!.GetHTMLTextAsync();

        if (UpdateState)
            await UpdateOrganizationAsync();
        else
            await AddOrganizationAsync();

        this.GoBack();
    }

    private async Task UpdateOrganizationAsync()
    {
        var response = await this.OrganizationService.UpdateAsync(organization!);

        if (response)
            this.PopupService.ShowSnackbarSuccess("Organizacija uspešno posodobljena");
        else
            this.PopupService.ShowSnackbarError("Prišlo je do napake, poskusite ponovno");
    }

    private async Task AddOrganizationAsync()
    {
        var orgResponse= await this.OrganizationService.AddAsync(organization!, UserId!);        

        if (orgResponse != null)
        {
            await this.OrganizationService.SetDefaultOrganizationAsync(DefaultOrganization.Create(orgResponse));
            this.PopupService.ShowSnackbarSuccess("Organizacija uspešno dodana");
        }
        else
            this.PopupService.ShowSnackbarError("Prišlo je do napake, poskusite ponovno");
    }

    private void GoBack() =>
        this.Navigation.NavigateTo("/");
}