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
    public string? Token { get; set; }

    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
 
    private UrlRegistration urlRegistration = new UrlRegistration();

    UrlRegistrationValidator urlRegistrationValidator = new UrlRegistrationValidator();    

    bool isShowPasword;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    bool isShowConfirmPasword;
    private InputType ConfirmPasswordInput = InputType.Password;
    private string ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;


    private MudForm? passwordForm;
   

    public async Task OnValidSubmit()
    {
        await passwordForm.Validate();

        if (!passwordForm.IsValid)
            return;
        
        _isBusy = true;

        var response = await this.AuthService.ConfirmRegistrationAsync(urlRegistration.Email, Token!, urlRegistration.Password);            
           
        this._errorMessage = response.Message;
        
        _isBusy = false;

        if (response.Succeeded)
            this.Navigation.NavigateTo("/organization");
    } 

    public void ButtonPasswordClick()
    {
        if (isShowPasword)
        {
            isShowPasword = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShowPasword = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }

    public void ButtonConfirmPasswordClick()
    {
        if (isShowConfirmPasword)
        {
            isShowConfirmPasword = false;
            ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmPasswordInput = InputType.Password;
        }
        else
        {
            isShowConfirmPasword = true;
            ConfirmPasswordInputIcon = Icons.Material.Filled.Visibility;
            ConfirmPasswordInput = InputType.Text;
        }
    }
}