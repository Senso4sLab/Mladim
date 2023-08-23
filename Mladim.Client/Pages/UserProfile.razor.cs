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
    public IPopupService PopupService { get; set; }


    [Inject]
    public NavigationManager Navigation { get; set; }



    private bool editableAccount = false;

    private AppUserVM appUser = new AppUserVM();

    private MudForm appUserForm;

    private AppUserValidator appUserValidator = new AppUserValidator();


    


    bool isShow;
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
    }


    private void CancelButton()
    {
        Navigation.NavigateTo("/");
    }

    private async Task UpdateAccountIfEditable(bool editState)
    {
        await appUserForm.Validate();

        if (!appUserForm.IsValid)
            return;

        this.editableAccount = editState;

        if (editState)
            return;              

        if (await this.AccountService.UpdateAccountAsync(appUser))
            this.PopupService.ShowSnackbarSuccess("Podatki so uspešno posodobljeni");
        else
        {
            this.PopupService.ShowSnackbarError();
            await OnInitializedAsync();
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

    public void ButtonNewPasswordClick()
    {
        if (isShow)
        {
            isShow = false;
            NewPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            NewPasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            NewPasswordInputIcon = Icons.Material.Filled.Visibility;
            NewPasswordInput = InputType.Text;
        }
    }


    public void ButtonConfirmedPasswordClick()
    {
        if (isShow)
        {
            isShow = false;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            ConfirmedPasswordInput = InputType.Password;
        }
        else
        {
            isShow = true;
            ConfirmedPasswordInputIcon = Icons.Material.Filled.Visibility;
            ConfirmedPasswordInput = InputType.Text;
        }
    }

}