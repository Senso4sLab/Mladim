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

using Mladim.Client.Services.Authentication;
using Mladim.Client.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Layouts;
using Blazored.TextEditor;
using MudBlazor;
using Mladim.Client.Services.SubjectServices.Contracts;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using Mladim.Client.Services.PopupService;
using Mladim.Domain.Dtos;
using Mladim.Client.Models;

namespace Mladim.Client.Pages;

public partial class Index
{

    [Inject]
    protected NavigationManager Navigation { get; set; }

    [Inject]
    protected IOrganizationService OrganizationService { get; set; }

    [Inject]
    protected IAuthService AuthService { get; set; }

    [Inject]
    protected IPopupService PopupService { get; set; }

    private List<OrganizationVM> organizations = new List<OrganizationVM>();

    private OrganizationVM selectedOrganization;

    protected async override Task OnInitializedAsync()
    {
        organizations = await GetOrganizationByUserIdAsync();

        if (organizations.Count == 0)
            return;

        var defaultOrg = await OrganizationService.DefaultOrganizationAsync();

        selectedOrganization = defaultOrg != null ? organizations.FirstOrDefault(o => o.Id == defaultOrg.Id) ?? organizations.FirstOrDefault()!
            : organizations?.FirstOrDefault()!;      

        await this.OrganizationService.SetDefaultOrganizationAsync(DefaultOrganization.Create(selectedOrganization));
    }


    private async Task OrganizationValueChanged(OrganizationVM organization)
    {
       selectedOrganization = organization;
       await this.OrganizationService.SetDefaultOrganizationAsync(DefaultOrganization.Create(selectedOrganization));    
    }

    private async Task<List<OrganizationVM>> GetOrganizationByUserIdAsync()
    {
        var userId = await this.AuthService.GetUserIdentityAsync();

        if(userId == null) 
            return new List<OrganizationVM>();    

        var organizations =  await this.OrganizationService.GetByUserIdAsync(userId);

        return organizations.ToList();
    }



    private async Task AddOrganizationAsync()
    {
        var userId = await this.AuthService.GetUserIdentityAsync();
        
        if(userId != null)
            this.Navigation.NavigateTo($"organization/{userId}");
    }

    private void EditOrganizationAsync(OrganizationVM organization) =>
         this.Navigation.NavigateTo($"organization/{organization.Id}");


    private async Task RemoveOrganizationAsync(OrganizationVM organization)
    {
        var response = await this.PopupService.ShowSimpleTextDialogAsync("Brisanje organizacije", "Ali želite izbrisati organizacijo?");

        if (!response)
            return;

        var succeedResponse = await this.OrganizationService.RemoveAsync(organization.Id);     

        if (succeedResponse)
        {
            this.PopupService.ShowSnackbarSuccess("Organizacija je uspešno odstranjena");
            organizations.Remove(organization);
            selectedOrganization = null;
           
        }
        else
            this.PopupService.ShowSnackbarError();
    }



}