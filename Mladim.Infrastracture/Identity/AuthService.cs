using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
using Mladim.Client.Extensions;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mladim.Infrastracture.Identity;

public class AuthService : IAuthService
{
    private UserManager<AppUser> UserManager { get; }

    private IAppUserRepository UserRepository { get; }
    private JwtSettings JwtSettings { get; }
    public AuthService(UserManager<AppUser> userManager, IAppUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
    {
        this.UserManager = userManager;
        this.UserRepository = userRepository;
        this.JwtSettings = jwtSettings.Value;       
    }
   
    public async Task<Result<AuthResponse>> LoginAsync(string email, string password)
    {
        
            var user = await this.UserRepository.FindByEmailAsync(email);

            if (user == null)
                return Result<AuthResponse>.Error("Vnešeni podatki so napačni");

            if (!await this.UserManager.CheckPasswordAsync(user, password))
                return Result<AuthResponse>.Error("Vnešeni podatki so napačni");

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email!,
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

        //foreach (var role in await UserManager.GetRolesAsync(user))
        //    tokenClaims.Add(new Claim(ClaimTypes.Role, role));


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


    //public async Task<bool> ExistClaimValueAsync(AppUser user, string claimValue)
    //{
    //    var claims = await this.UserManager.GetClaimsAsync(user);
    //    return claims.Any(c => c.Value == claimValue);
    //}
   

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
        bool isEmailConfirmed = await this.UserManager.IsEmailConfirmedAsync(user);
        
        if (isEmailConfirmed)
            return true;
        
        var identityResult = await this.UserManager.ConfirmEmailAsync(user, emailToken);
        return identityResult.Succeeded;
    }


    public async Task<string> EmailTokenAsync(AppUser appUser)
    {
        return await this.UserManager.GenerateEmailConfirmationTokenAsync(appUser);
    }


    public async Task<Result<AuthResponse>> RegisterConfirmationAsync(string email, string emailToken, string password)
    {

        emailToken = emailToken.Replace(' ', '+');

        var user = await this.UserManager.FindByEmailAsync(email);

        if(user == null)
            return Result<AuthResponse>.Error("Uporabnik ne obstaja.");     

        if(!await ConfirmEmailAsync(user, emailToken))
            return Result<AuthResponse>.Error("Potrditev registracije ni uspela.");


        var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        var result = await UserManager.ResetPasswordAsync(user, token, password);

        if (!result.Succeeded)
            return Result<AuthResponse>.Error("Gesla ni mogoče spremeniti! Preveri, ali vsebuje vsaj vsaj eno veliko črko, eno malo črko, eno številko in en poseben znak.");
     

        var authResponse = new AuthResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email!,
            Token = await CreateTokenAsync(user),
        };

        return Result<AuthResponse>.Success(authResponse);
    }


    public async Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null)  
    {
        var user = await this.UserRepository.FindByEmailAsync(email);

        if (user != null)
            return Result<RegistrationResponse>.Error("Uporabnik že obstaja");

        var appUser = AppUser.Create(name, surname, nickname, email, email);
        var response = await this.UserRepository.CreateAsync(appUser, password ?? GenerateUserPassword());     

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
