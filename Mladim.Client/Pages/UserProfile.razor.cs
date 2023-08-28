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
using System.Security.Claims;
using Mladim.Domain.IdentityModels;
using Mladim.Client.Services.AccountService;
using Mladim.Client.Services.PopupService;
using Mladim.Domain.Models;
using Mladim.Client.Validators;

namespace Mladim.Client.Pages;

public partial class UserProfile
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Inject]
    protected IAccountService AccountService { get; set; }

    [Inject]
    protected IAuthService AuthService { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }


    [Inject]
    public NavigationManager Navigation { get; set; } = default!;



    private bool editableAccount = false;
    private bool editablePassword = false;

    private AppUserVM appUser = new AppUserVM();

    private UserPassword userPassword = new UserPassword();

    private MudForm appUserForm;
    private MudForm userPasswordForm;

    private AppUserValidator appUserValidator = new AppUserValidator();    
    private UserPasswordValidator userPasswordValidator = new UserPasswordValidator();

    
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private InputType NewPasswordInput = InputType.Password;
    private string NewPasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    private InputType ConfirmedPasswordInput = InputType.Password;
    private string ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;



    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is null)
            return;

        var authState = await authenticationState;
        var userId = authState?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(userId is not null)        
            appUser = await this.AccountService.GetAccountByIdAsync(userId);

        this.editableAccount = false;
        this.editablePassword = false;
    }


    private async Task CancelButton()
    {
        await OnInitializedAsync();
    }

    private async Task UpdateAccountAsync(bool editState)
    {
        await appUserForm.Validate();

        if (!appUserForm.IsValid)
            return;

        this.editableAccount = editState;

        if (editState)
            return;

        if (await this.AccountService.UpdateAccountAsync(appUser))
        {
            this.PopupService.ShowSnackbarSuccess("Podatki so uspešno posodobljeni");            
        }
        else
            this.PopupService.ShowSnackbarError();
    }


    private async Task UpdatePasswordAsync(bool editState)
    {
        if (editState)
        {
            this.editablePassword = editState;
            return;
        }

        await userPasswordForm.Validate();

        if (!userPasswordForm.IsValid)
            return;

        var response = await this.AuthService.ChangePasswordAsync(appUser.Id, userPassword.NewPassword);

        if (response.Succeeded)
        {
            this.PopupService.ShowSnackbarSuccess("Podatki so uspešno posodobljeni");
            this.editablePassword = editState;
        }
        else
            this.PopupService.ShowSnackbarError(response.Message);
    }






    bool isShowPassword;
    public void ButtonPasswordClick()
    {
        
        if (isShowPassword)
        {
            isShowPassword = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            isShowPassword = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }


    bool isShowNewPassword;
    public void ButtonNewPasswordClick()
    {
        if (isShowNewPassword)
        {
            isShowNewPassword = false;
            NewPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            NewPasswordInput = InputType.Password;
        }
        else
        {
            isShowNewPassword = true;
            NewPasswordInputIcon = Icons.Material.Filled.Visibility;
            NewPasswordInput = InputType.Text;
        }
    }

    bool isShowConfirmedPassword;
    public void ButtonConfirmedPasswordClick()
    {
        if (isShowConfirmedPassword)
        {
            isShowConfirmedPassword = false;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmedPasswordInput = InputType.Password;
        }
        else
        {
            isShowConfirmedPassword = true;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.Visibility;
            ConfirmedPasswordInput = InputType.Text;
        }
    }

}