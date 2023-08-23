using Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using MudBlazor;


using Mladim.Domain.Models;
using Mladim.Client.Validators;
using Mladim.Client.Services.AccountService;

namespace Mladim.Client.Pages;

public partial class Login
{

    [Inject]
    public IAuthService AuthService { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }


    [Inject]
    public IAccountService AccountService { get; set; }

    [Parameter]
    public string? UserId { get; set; }


    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
    private LoginUser loginUser = new LoginUser();
    private UserPassword userPassword = new UserPassword();

    bool isShow;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private MudForm? loginForm;

    LoginUserValidator loginUserValidator = new LoginUserValidator();


    private bool IsPasswordChanged => !string.IsNullOrEmpty(UserId);
    public async Task OnValidSubmit()
    {
        try
        {
            await loginForm.Validate();

            if (!loginForm.IsValid)
                return;

            _isBusy = true;
            await LoginAsync();          
            this.Navigation.NavigateTo("/");                
        }       
        catch (Exception ex)
        {
            _isBusy = false;
            this._errorMessage = ex.Message;
        }
        finally
        { 
            _isBusy = true; 
        }
    }



    private async Task LoginAsync()
    {

        var response = IsPasswordChanged ? await this.AuthService.ChangePasswordAsync(userPassword) :
            await this.AuthService.LoginAsync(loginUser);

        if(!response.Succeeded)
            throw new Exception(response.Message);  
    }

    protected async override Task OnInitializedAsync()
    {
        if (IsPasswordChanged)
        {
            var appUser = await AccountService.GetAccountByIdAsync(UserId);

            loginUser = new LoginUser { Email = appUser.Email, Password = string.Empty };
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