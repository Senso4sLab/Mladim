using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Models;
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
    private JwtSettings JwtSettings { get; }
    public AuthService(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        this.JwtSettings = jwtSettings.Value;
        this.UserManager = userManager;
    }
   
    public async Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser)
    {
        var user = await this.UserManager.FindByEmailAsync(loginUser.Email);

        if (user == null)
            return Result<AuthResponse>.Error("Vnešeni podatki so napačni");            

        bool passwordCorrect = await this.UserManager.CheckPasswordAsync(user, loginUser.Password);

        if (!await this.UserManager.CheckPasswordAsync(user, loginUser.Password))
            return Result<AuthResponse>.Error("Vnešeni podatki so napačni");

        var authResponse =  new AuthResponse
        {
            Id     = user.Id,
            Name   = user.Name,
            Email  = user.Email!,
            Token  = await CreateTokenAsync(user),
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


    public async Task<AppUser?>ExistAppUserAsync(string email)
    {         
        return await this.UserManager.FindByEmailAsync(email);          
    }
    
    public async Task<bool> IsAdminAsync(AppUser user, int organizationId)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);
        return claims.Any(claim => claim.Type == nameof(ApplicationClaim.Admin) && claim.Value == organizationId.ToString());
    }

    public async Task<bool> IsEmailConfirmedAsync(AppUser user)
    {
       return await this.UserManager.IsEmailConfirmedAsync(user);
    }


    public string GenerateAppUserPassword()
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


    public async Task<bool> ExistClaimAsync(AppUser user, string claimValue)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);

        return claims.Any(c => c.Value == claimValue);
    }



    public async Task CreateUserWithClaimAsync(string name, string surname, string email, Claim claim)
    {
        var user = RegistrationUser.Create(name, surname, name, email, GenerateAppUserPassword());
        var registrationResponse = await RegisterAsync(user);

        if(!registrationResponse.Succeeded)
            throw new Exception(registrationResponse.Message);

        await UpsertClaimAsync(registrationResponse.Value!, claim);        
    }

    

    public async Task UpsertClaimAsync(AppUser user, Claim newClaim)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);
        
        var foundClaim = claims.FirstOrDefault(c => c.Value == newClaim.Value);

        var identityResult =  foundClaim == null ? await this.UserManager.AddClaimAsync(user, newClaim) :
            await this.UserManager.ReplaceClaimAsync(user, foundClaim, newClaim);

        if (!identityResult.Succeeded)
            throw new Exception(string.Join(", ", identityResult.Errors.Select(e => e.Description)));       
       
    }   


    public async Task<Result<AppUser>> RegisterAsync(RegistrationUser request)
    {
        var user = await this.UserManager.FindByEmailAsync(request.Email);

        if (user != null)
            return Result<AppUser>.Error("Uporabnik že obstaja");

        var appUser = AppUser.Create(request.Name, request.Surname, request.Nickname, request.Email, request.Email);       

        var result = await this.UserManager.CreateAsync(appUser, request.Password);
        

        if (!result.Succeeded)
            return Result<AppUser>.Error(string.Join(", ", result.Errors.Select(e => e.Description)));
        
        var registrationResponse = new RegistrationResponse { UserId = appUser.Id,};       

        return Result<AppUser>.Success(appUser);       
    } 
}
