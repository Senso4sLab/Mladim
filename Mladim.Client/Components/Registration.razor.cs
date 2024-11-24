using global::Microsoft.AspNetCore.Components;
using Mladim.Client.Services.Authentication;
using MudBlazor;
using Mladim.Client.Validators;
using Mladim.Domain.Models;

using System.Text.Json;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Json;
using static Mladim.Client.Pages.Enka;

namespace Mladim.Client.Components;

public partial class Registration
{
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Inject]
    public IHttpClientFactory HttpFactory { get; set; } = default!;

    [Parameter]
    public string? Token { get; set; }

    private bool _isBusy = false;
    private string _errorMessage = string.Empty;
    private bool _checked1ka = false;
 
    private UrlRegistration urlRegistration = new UrlRegistration();

    UrlRegistrationValidator urlRegistrationValidator = new UrlRegistrationValidator();

    bool isShowPasword;
    private InputType PasswordInput = InputType.Password;
    private string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

    bool isShowConfirmPasword;
    private InputType ConfirmPasswordInput = InputType.Password;
    private string ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;


    private MudForm? passwordForm;

    private HttpClient httpClient;
    protected override void OnInitialized()
    {
        httpClient = HttpFactory.CreateClient("1kaHttpClient");
    }

    public async Task OnValidSubmit()
    {
        await passwordForm.Validate();

        if (!passwordForm.IsValid)
            return;

        _isBusy = true;

        var response = await this.AuthService.ConfirmRegistrationAsync(urlRegistration.Name, urlRegistration.Email, Token?.Replace(' ', '+')!, urlRegistration.Password, _checked1ka);

        //if (response.Succeeded && _checked1ka)        
        //await UserConfirmRegisterin1kaAsync(urlRegistration.Name, urlRegistration.Email, urlRegistration.Password, urlRegistration.ConfirmPassword);
        //

        if (response.Succeeded && _checked1ka)
            await UserLogin1kaAsync(urlRegistration.Email, urlRegistration.Password);


        this._errorMessage = response.Message;

        if (response.Succeeded)
            this.Navigation.NavigateTo("/organization");

        _isBusy = false;  
            
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

    private async Task<string> UserRegisterin1kaAsync(string ime, string email, string geslo, string geslo2, int agree = 1, string submit = "Registracija")
    {
        var user = new Register1kaUser(ime, email, geslo, geslo2, agree, submit);

        var userJson = JsonSerializer.Serialize(user);

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = true,
        };

        using var httpClient = new HttpClient(handler);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://mladim1ka.azurewebsites.net/1ka/frontend/api/api.php?action=register")
        {
            Content = new StringContent(userJson),
        };

        message.Content.Headers.Add("Content-Length", $"{userJson.Length}");

        var response = await httpClient.SendAsync(message);

        if (response.IsSuccessStatusCode)
            return string.Empty;

        var querylocation = response.Headers.Location?.Query!;

        if (querylocation.Contains("existing_ime=1"))
            return "Vnešeno ime že obstaja";

        if (querylocation.Contains("existing_email=1"))
            return "Vnešen email že obstaja";

        return string.Empty;

    }

    public record Register1kaUser(string ime, string email, string geslo, string geslo2, int agree, string submit);



    private async Task<bool> UserConfirmRegisterin1kaAsync(string ime, string email, string geslo, string geslo2, int language = 1)
    {
        var user = new RegisterConfirm1kaUser(ime, email, geslo, geslo2, language);

        var userJson = JsonSerializer.Serialize(user);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "frontend/api/api.php?action=register_confirm")
        {
            Content = new StringContent(userJson),
        };       

        var response = await httpClient.SendAsync(message);

        return response.IsSuccessStatusCode;
    }

    
    public record RegisterConfirm1kaUser(string ime, string email, string geslo, string geslo2, int language);

    private async Task<bool> UserLogin1kaAsync(string email, string geslo)
    {
        var user = new Login1kaUser(email, geslo, "Prijava");

        var userJson = JsonSerializer.Serialize(user);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "frontend/api/api.php?action=login")
        {
            Content = new StringContent(userJson),
        };

        var response = await httpClient.SendAsync(message);

        return response.IsSuccessStatusCode;
    }


    public record Login1kaUser(string email, string pass, string submit);

}