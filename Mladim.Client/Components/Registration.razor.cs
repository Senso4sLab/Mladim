using global::System;
using global::System.Collections.Generic;
using global::System.Linq;
using global::System.Threading.Tasks;
using global::Microsoft.AspNetCore.Components;
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
using Syncfusion.Blazor;
using Syncfusion.Blazor.RichTextEditor;
using Syncfusion.Blazor.Gantt;
using Mladim.Client.Services.AccountService;
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
 
    private UserPassword userPassword = new UserPassword();

    UserPasswordValidator userPasswordValidator = new UserPasswordValidator();    

    

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

        var response = await this.AuthService.ChangePasswordAsync(userPassword);
        this._errorMessage = response.Message;
        
        _isBusy = false;
          
        if(response.Succeeded)
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