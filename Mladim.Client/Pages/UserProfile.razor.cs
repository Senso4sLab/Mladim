using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Mladim.Client.Services.Authentication;
using Mladim.Client.ViewModels;
using MudBlazor;
using System.Security.Claims;
using Mladim.Client.Services.AccountService;
using Mladim.Client.Services.PopupService;
using Mladim.Domain.Models;
using Mladim.Client.Validators;
using Mladim.Client.Services.FileService;
using Mladim.Client.ViewModels.Organization;
using Microsoft.AspNetCore.Components.Forms;

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
    IFileService FileService { get; set; } = default!;


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

    public async Task UploadUserImageAsync(IBrowserFile e)
    {
        var image = await GenerateByteImageAsync(e);

        if(appUser.ImageUrl != null)
        {
            var response = await this.FileService.DeleteFileAsync(appUser.ImageUrl);
            appUser.ImageUrl = null;
        }

        var url = await this.FileService.AddUserProfileImageAsync(this.appUser.Id, image.data, image.name);

        this.appUser.ImageUrl = url;      

        this.StateHasChanged();
    }

    private async Task<(string name, List<byte> data)> GenerateByteImageAsync(IBrowserFile e)
    {
        var resizedImage = await e.RequestImageFileAsync(e.ContentType, 200, 200);

        var buffer = new byte[resizedImage.Size];
        await resizedImage.OpenReadStream().ReadAsync(buffer);

        string fileName = Path.GetFileName(resizedImage.Name);

        return (fileName, buffer.ToList());
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
            this.PopupService.ShowSnackbarSuccess("Podatki so uspešno posodobljeni");        
        else
            this.PopupService.ShowSnackbarError();
    }


    private async Task ChangePasswordAsync(bool editState)
    {
        if (editState)
        {
            this.editablePassword = editState;
            return;
        }

        await userPasswordForm.Validate();

        if (!userPasswordForm.IsValid)
            return;

        var passwordChanged = await this.AuthService.TryChangePasswordAsync(appUser.Id, userPassword.OldPassword, userPassword.NewPassword);

        if (passwordChanged)
        {
            this.PopupService.ShowSnackbarSuccess("Geslo je uspešno spremenjeno");
            this.editablePassword = editState;
        }
        else
            this.PopupService.ShowSnackbarError("Gesla ni mogoče spremeniti");
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