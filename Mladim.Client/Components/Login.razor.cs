using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using MudBlazor;
using Mladim.Client.Services.AccountService;
using Mladim.Client.Validators;
using Mladim.Domain.Models;

namespace Mladim.Client.Components;

public partial class Login
{
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
      
    

    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
    private LoginUser loginUser = new LoginUser();
    

    bool isShow;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private MudForm? loginForm;

    LoginUserValidator loginUserValidator = new LoginUserValidator();   
    
    public async Task OnValidSubmit()
    {

        await loginForm.Validate();

        if (!loginForm.IsValid)
            return;

        _isBusy = true;

        var response = await this.AuthService.LoginAsync(loginUser);

        _isBusy = false;

        this._errorMessage = response.Message;

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