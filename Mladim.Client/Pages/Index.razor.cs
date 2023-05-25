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
using Mladim.Client.Models;
using Mladim.Client.Layouts;
using Blazored.TextEditor;
using MudBlazor;
using Mladim.Client.Services.SubjectServices.Contracts;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using Mladim.Client.Services.PopupService;
using Mladim.Domain.Dtos;

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

    private List<Organization> organizations = new List<Organization>();

    private Organization selectedOrganization;

    protected async override Task OnInitializedAsync()
    {
        organizations = await GetOrganizationByUserIdAsync();

        if (organizations.Count == 0)
            return;

        var orgId = await OrganizationService.DefaultOrganizationIdAsync();

        selectedOrganization = orgId != null ? organizations.FirstOrDefault(o => o.Id == orgId) ?? organizations.FirstOrDefault()!
            : organizations?.FirstOrDefault()!;      

        await this.OrganizationService.SetDefaultOrganizationAsync(selectedOrganization!.Id);
    }


    private async Task OrganizationValueChanged(Organization organization)
    {
       selectedOrganization = organization;
       await this.OrganizationService.SetDefaultOrganizationAsync(organization.Id);    
    }





    private async Task<List<Organization>> GetOrganizationByUserIdAsync()
    {
        var userId = await this.AuthService.GetUserIdentityAsync();

        if(userId == null) 
            return new List<Organization>();    

        var organizations =  await this.OrganizationService.GetByUserIdAsync(userId);

        return organizations.ToList();
    }



    private async Task AddOrganizationAsync()
    {
        var userId = await this.AuthService.GetUserIdentityAsync();
        
        if(userId != null)
            this.Navigation.NavigateTo($"organization/{userId}");
    }

    private void EditOrganizationAsync(Organization organization) =>
         this.Navigation.NavigateTo($"organization/{organization.Id}");


    private async Task RemoveOrganizationAsync(Organization organization)
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