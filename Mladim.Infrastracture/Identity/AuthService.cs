using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static MudBlazor.Colors;

namespace Mladim.Infrastracture.Identity;

public class AuthService : IAuthService
{
    private UserManager<AppUser> UserManager { get; }   

    public IUnitOfWork UnitOfWork { get; }
    private JwtSettings JwtSettings { get; }
    public AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
    {
        this.UserManager = userManager;
        this.UnitOfWork = unitOfWork;
        this.JwtSettings = jwtSettings.Value;       
    }
   
    public async Task<Result<AuthResponse>> LoginAsync(string email, string password)
    {      
            var user = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(email);

            if (user == null)
                return Result<AuthResponse>.Error("Vnešeni podatki so napačni");

            if (!await this.UserManager.CheckPasswordAsync(user, password))
                return Result<AuthResponse>.Error("Vnešeni podatki so napačni");


            if(user.Mladim1ka)
            {
                var response = await LoginUser1kaAsync(email, password);
                if (response != string.Empty)
                    return Result<AuthResponse>.Error(response);
            }

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email!,
                Mladim1ka = user.Mladim1ka,
                Token = await CreateTokenAsync(user),
            };

            return Result<AuthResponse>.Success(authResponse);       
    
    }

    private async Task<string> CreateTokenAsync(AppUser user)
    {
        List<Claim> tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };      


        tokenClaims.AddRange(await this.UserManager.GetClaimsAsync(user));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));

        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                claims: tokenClaims,
                signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }        
    public async Task<bool> HasUserRoleAsync(AppUser user, string roleValue)
    {     
       var roles = await this.UserManager.GetClaimsAsync(user);
       return roles.Any(r => r.Type == ClaimTypes.Role && r.Value == roleValue);   
    }
    public async Task<bool> AddUserRoleAsync(string userId, string roleValue)
    { 
        if (!Enum.TryParse<ApplicationRole>(roleValue, out _))
            throw new ArgumentException("Application role type is not defined");

        var user = await this.UserManager.FindByIdAsync(userId);

        ArgumentNullException.ThrowIfNull(user);

        if (await HasUserRoleAsync(user, roleValue))
           return false;

        Claim claim = new Claim(ClaimTypes.Role, roleValue);
        var identityResult = await this.UserManager.AddClaimAsync(user, claim);

       return identityResult.Succeeded;
    }   

    public async Task<bool> AddClaimAsync(AppUser user, Claim newClaim)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);
        
        if (claims.FirstOrDefault(c => c.Value == newClaim.Value) != null)
            return false;

        var claimResponse = await this.UserManager.AddClaimAsync(user, newClaim); 

        return claimResponse.Succeeded;
    }

    public async Task<bool> ReplaceClaimAsync(AppUser user, Claim newClaim)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);

        var foundClaim = claims.FirstOrDefault(c => c.Value == newClaim.Value);

        if (foundClaim == null)
            return false;

        if (foundClaim!.ToString() == newClaim.ToString())
            return false;

        var claimResponse = await this.UserManager.ReplaceClaimAsync(user, foundClaim, newClaim);
        
        return claimResponse.Succeeded;
    }    
    private async Task<bool> ConfirmEmailAsync(AppUser user, string emailToken)
    {        
        if (await this.UserManager.IsEmailConfirmedAsync(user))
            return true;
        
        var identityResult = await this.UserManager.ConfirmEmailAsync(user, emailToken);
        return identityResult.Succeeded;
    }


    public Task<string> GenerateEmailTokenAsync(AppUser appUser) =>
        this.UserManager.GenerateEmailConfirmationTokenAsync(appUser);


    public async Task<bool> ResetPasswordAsync(AppUser user, string token, string password)
    {
        var response = await this.UserManager.ResetPasswordAsync(user, token, password);
        return response.Succeeded;
    }      


    public Task<string> GeneratePasswordResetTokenAsync(AppUser user) =>
        this.UserManager.GeneratePasswordResetTokenAsync(user);


    public async Task<Result<AuthResponse>> RegisterConfirmationAsync(string name, string email, string emailToken, string password, bool mladim1ka)
    {      

        var user = await this.UserManager.FindByEmailAsync(email);

        if(user == null)
            return Result<AuthResponse>.Error("Uporabnik ne obstaja.");

        if (!await ConfirmEmailAsync(user, emailToken))
            return Result<AuthResponse>.Error("Potrditev registracije ni uspela.");
               

        if (mladim1ka)
        {
            var response1ka = await UserRegisterin1kaAsync(name, email, password, password);

            if (!string.IsNullOrEmpty(response1ka))
                return Result<AuthResponse>.Error(response1ka);

           if(!await UserConfirmRegisterin1kaAsync(name, email, password, password))
                return Result<AuthResponse>.Error("Registracija ni uspela");

            user.Mladim1ka = true;
        }

        var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        var result = await UserManager.ResetPasswordAsync(user, token, password);

        if (!result.Succeeded)
            return Result<AuthResponse>.Error("Gesla ni mogoče spremeniti! Preveri, ali vsebuje vsaj eno veliko črko, eno malo črko, eno številko in en poseben znak.");

        user.Name = name;
        await UserManager.UpdateAsync(user);        

        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email!,
            Mladim1ka = user.Mladim1ka,
            Token = await CreateTokenAsync(user),
        };

        return Result<AuthResponse>.Success(authResponse);
    }

    public async Task<Result> ChangePasswordrequestAsync(AppUser user, string oldPassword, string newPassword)
    {
        var result = await UserManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
            return Result.Error("Gesla ni mogoče spremeniti! Preveri, ali vsebuje vsaj eno veliko črko, eno malo črko, eno številko in en poseben znak.");

        if (await UserRequestNewPassword1kaAsync(user.Email!, newPassword))
            return Result.Success();
        else
            return Result.Error("Gesla ni mogoče spremeniti!");

    }


    public async Task<Result> ResetPasswordRequestAsync(AppUser user, string token, string password)
    {
        var result = await UserManager.ResetPasswordAsync(user, token, password);    

        if (!result.Succeeded)
            return Result.Error("Gesla ni mogoče spremeniti! Preveri, ali vsebuje vsaj eno veliko črko, eno malo črko, eno številko in en poseben znak.");

        if(user.Mladim1ka )
        {
            var response  = await UserRequestNewPassword1kaAsync(user.Email, password);

            if(!response)
            {
                return Result.Error("Gesla ni mogoče spremeniti!");
            }
        }      

        return Result.Success();
    }



    private async Task<bool> UserRequestNewPassword1kaAsync(string email, string password)
    {
        var newPass = new NewPassword1kaUser(email, password);

        var userJson = JsonSerializer.Serialize(newPass);

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = false,
        };

        using var httpClient = new HttpClient(handler);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://mladim1ka.azurewebsites.net/1ka/frontend/api/api.php?action=reset_password")
        {
            Content = new StringContent(userJson),
        };

        message.Content.Headers.Add("Content-Length", $"{userJson.Length}");

        var response = await httpClient.SendAsync(message);

        var querylocation = response.Headers.Location?.Query!;

        if (querylocation is not null && querylocation.Contains("success=1"))
            return true;

        return false;      
    }


    private async Task<string> LoginUser1kaAsync(string email, string geslo)
    {
        var user = new NewPassword1kaUser(email, geslo);

        var userJson = JsonSerializer.Serialize(user);

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = false,
            
        };

        using var httpClient = new HttpClient(handler);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://mladim1ka.azurewebsites.net/1ka/frontend/api/api.php?action=login")
        {
            Content = new StringContent(userJson),
        };

        message.Content.Headers.Add("Content-Length", $"{userJson.Length}");

        var response = await httpClient.SendAsync(message);      

        var stringContent = await response.Content.ReadAsStringAsync();

        if (stringContent.Contains("class=\"red\""))
            return "Vnešeni podatki niso pravilni";

        return string.Empty;        
    }


    private async Task<string> UserRegisterin1kaAsync(string ime, string email, string geslo, string geslo2, int agree = 1, string submit = "Registracija")
    {
        var user = new Register1kaUser(ime, email, geslo, geslo2, agree, submit);

        var userJson = JsonSerializer.Serialize(user);

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = false,
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


    private async Task<bool> UserConfirmRegisterin1kaAsync(string ime, string email, string geslo, string geslo2, int language = 1)
    {
        var user = new RegisterConfirm1kaUser(ime, email, geslo, geslo2, language);

        var userJson = JsonSerializer.Serialize(user);

        var handler = new HttpClientHandler()
        {
            AllowAutoRedirect = false,
        };

        using var httpClient = new HttpClient(handler);

        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://mladim1ka.azurewebsites.net/1ka/frontend/api/api.php?action=register_confirm")
        {
            Content = new StringContent(userJson),
        };

        message.Content.Headers.Add("Content-Length", $"{userJson.Length}");

        var response = await httpClient.SendAsync(message);

        return response.StatusCode == System.Net.HttpStatusCode.Found;       
    }


    public record RegisterConfirm1kaUser(string ime, string email, string geslo, string geslo2, int language);

    public record NewPassword1kaUser(string email, string pass);
  

    public record Register1kaUser(string ime, string email, string geslo, string geslo2, int agree, string submit);


    public async Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null)  
    {
        var user = await this.UnitOfWork.AppUserRepository.FindByEmailAsync(email);

        if (user != null)
            return Result<RegistrationResponse>.Error("Uporabnik že obstaja");

        var appUser = AppUser.Create(name, surname, nickname, email, email);
        var response = await this.UnitOfWork.AppUserRepository.CreateAsync(appUser, password ?? GenerateUserPassword());     

        if (response.Succeeded)
            return Result<RegistrationResponse>.Success(new RegistrationResponse { UserId = response.Value!.Id });
        else
            return Result<RegistrationResponse>.Error(response.Message);            
    }

    private string GenerateUserPassword()
    {
        var options = this.UserManager.Options.Password;

        int length = options.RequiredLength;

        bool nonAlphanumeric = options.RequireNonAlphanumeric;
        bool digit = options.RequireDigit;
        bool lowercase = options.RequireLowercase;
        bool uppercase = options.RequireUppercase;

        StringBuilder password = new StringBuilder();
        Random random = new Random();

        while (password.Length < length)
        {
            char c = (char)random.Next(32, 126);

            password.Append(c);

            if (char.IsDigit(c))
                digit = false;
            else if (char.IsLower(c))
                lowercase = false;
            else if (char.IsUpper(c))
                uppercase = false;
            else if (!char.IsLetterOrDigit(c))
                nonAlphanumeric = false;
        }

        if (nonAlphanumeric)
            password.Append((char)random.Next(33, 48));
        if (digit)
            password.Append((char)random.Next(48, 58));
        if (lowercase)
            password.Append((char)random.Next(97, 123));
        if (uppercase)
            password.Append((char)random.Next(65, 91));

        return password.ToString();
    }


}
