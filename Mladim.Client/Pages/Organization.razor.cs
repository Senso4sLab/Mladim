using Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Services.PopupService;
using Mladim.Client.Models;
using Mladim.Client.ViewModels.Organization;
using Mladim.Client.Components.Organizations;

namespace Mladim.Client.Pages;

public partial class Organization
{

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    [Inject]
    protected IOrganizationService OrganizationService { get; set; } = default!;

    [Inject]
    protected IAuthService AuthService { get; set; } = default!;

    [Inject]
    protected IPopupService PopupService { get; set; } = default!;

    private List<OrganizationVM> organizations = new List<OrganizationVM>();

    [CascadingParameter]
    public OrganizationVM? SelectedOrganization { get; set; }

    [CascadingParameter]
    public EventCallback<int> OnSelectedOrganizationChanged { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await LoadOrganizationsByUserIdAsync();

        //var lastSelectedOrg = await OrganizationService.DefaultOrganizationAsync();

        //if (SelectedOrganization == null && organizations.)
        //    OrganizationValueChanged()

        //if(selectedOrganization != null)
        //    await this.OrganizationService.SetDefaultOrganizationAsync(DefaultOrganization.Create(selectedOrganization));
    }

    

    //private OrganizationVM? FindSelectedOrganization(DefaultOrganization? lastSelectedOrg) =>    
    //    lastSelectedOrg != null ? organizations.FirstOrDefault(o => o.Id == lastSelectedOrg.Id) ?? organizations.FirstOrDefault()
    //        : organizations?.FirstOrDefault();    


    private async Task OrganizationValueChanged(OrganizationVM organization)
    {
        await OnSelectedOrganizationChanged.InvokeAsync(organization.Id);       
    }

    private async Task LoadOrganizationsByUserIdAsync()
    {
        var userId = await this.AuthService.GetUserIdentityAsync();

        if (userId is null)
            organizations.Clear();
        else
            organizations = new List<OrganizationVM>(await this.OrganizationService.GetByUserIdAsync(userId));           
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
        var response = await this.PopupService.ShowSimpleTextDialogAsync("Brisanje organizacije", "Ali �elite izbrisati organizacijo?");

        if (!response)
            return;

        var succeedResponse = await this.OrganizationService.RemoveAsync(organization.Id);     

        if (succeedResponse)
        {
            this.PopupService.ShowSnackbarSuccess("Organizacija je uspe�no odstranjena");
            organizations.Remove(organization);
            await OnSelectedOrganizationChanged.InvokeAsync(0);
        }
        else
            this.PopupService.ShowSnackbarError();
    }
}