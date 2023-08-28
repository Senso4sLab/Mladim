using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using MudBlazor;
using Mladim.Client.Validators;
using Mladim.Domain.Models;

namespace Mladim.Client.Components;

public partial class Registration
{
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
      

    [Parameter]
    public string? UserId { get; set; }

    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
 
    private UrlRegistration urlRegistration = new UrlRegistration();

    UrlRegistrationValidator urlRegistrationValidator = new UrlRegistrationValidator();    

    

    bool isShow;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private MudForm? passwordForm;
   
    public async Task OnValidSubmit()
    {
        await passwordForm.Validate();

        if (!passwordForm.IsValid)
            return;
        
        _isBusy = true;

        var response = await this.AuthService.ChangePasswordAsync(UserId!, urlRegistration.Password);
        this._errorMessage = response.Message;
        
        _isBusy = false;

        if (response.Succeeded)
            this.Navigation.NavigateTo("/");
    } 

    public void ButtonPasswordClick()
    {
        if (isShow)
        {
            isShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
}