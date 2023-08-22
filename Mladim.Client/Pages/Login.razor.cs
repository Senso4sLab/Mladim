using Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using MudBlazor;


using Mladim.Domain.Models;
using Mladim.Client.Validators;

namespace Mladim.Client.Pages;

public partial class Login
{

    [Inject]
    public IAuthService AuthService { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }


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
        try
        {
            await loginForm.Validate();

            if (!loginForm.IsValid)
                return;

            _isBusy = true;
            var response = await this.AuthService.LoginAsync(loginUser);
            _isBusy = false;

            if(response.Succeeded)
                this.Navigation.NavigateTo("/");
            else
                this._errorMessage = response.Message;           
        }       
        catch (Exception ex)
        {
            _isBusy = false;
            this._errorMessage = ex.Message;
        }
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