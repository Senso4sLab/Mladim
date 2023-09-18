using Microsoft.AspNetCore.Components;
using Mladim.Client.ViewModels;
using Mladim.Client.Services.SubjectServices.Contracts;
using Mladim.Client.Services.PopupService;


namespace Mladim.Client.Components.Organizations.MemberTabs;

public partial class StaffMemberTab
{
    [Inject]
    protected IStaffMemberService StaffService { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    [Parameter]
    public OrganizationVM Organization { get; set; }


    private bool IsActive = true;

    private List<StaffMemberVM> Staff = new List<StaffMemberVM>();


    protected async override Task OnParametersSetAsync()
    {
       
        this.Staff = new List<StaffMemberVM>(await GetStaffByOrganizationId());
    }
        
    

    private Task<IEnumerable<StaffMemberVM>> GetStaffByOrganizationId() =>
        this.StaffService.GetByOrganizationIdAsync(this.Organization.Id, this.IsActive);
   

    private async Task AddStaffMemberAsync()
    {
        var staffMember = new StaffMemberVM();
       

        var dialogResponse = await this.PopupService.ShowStaffMemberDialog("Nov uporabnik", staffMember);

        if (!dialogResponse)
            return;

        staffMember = await this.StaffService.AddAsync(this.Organization.Id, staffMember);       

        if (staffMember != null)
        {
            Staff.Add(staffMember);
            this.PopupService.ShowSnackbarSuccess("Uporabnik uspešno dodan");
        }
        else
            this.PopupService.ShowSnackbarError();
    }

    private async Task CheckedChangedAsync(bool isActive)
    {
        this.IsActive = isActive;
        await OnParametersSetAsync();
    }

    private async Task UpdateStaffMemberAsync(StaffMemberVM staffMember)
    {

        var dialogResponse = await this.PopupService.ShowStaffMemberDialog("Uredi uporabnika", staffMember);

        if (!dialogResponse)
            return;

        var succeedResponse = await this.StaffService.UpdateAsync(staffMember);

        if (succeedResponse)
        {
            this.PopupService.ShowSnackbarSuccess("Podatki uspešno posodobljeni");
            await OnParametersSetAsync();
        }
        else
            this.PopupService.ShowSnackbarError();

    }

    private string RowStyleFunc(StaffMemberVM sm, int index)
    {
        return index %2 == 0 ? "background-color:white" : "background-color:#EFEFEF;";
    }


}