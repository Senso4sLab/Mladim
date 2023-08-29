using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Contracts.Persistence;
using Mladim.Application.Models;
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
    public AuthService(UserManager<AppUser> userManager, IAppUserRepository userRepository ,IOptions<JwtSettings> jwtSettings)
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
    
    public async Task<bool> IsUserAdminAsync(AppUser user, int organizationId)
    {
        var roles = await this.UserManager.GetRolesAsync(user);
        return roles.Any(r => r == "Admin");  
    }
    public async Task<bool> ExistClaimValueAsync(AppUser user, string claimValue)
    {
        var claims = await this.UserManager.GetClaimsAsync(user);
        return claims.Any(c => c.Value == claimValue);
    }
    //public async Task<AppUser> CreateUserWithClaimAsync(string name, string surname, string email, Claim claim)
    //{
    //    var user = UserRegistration.Create(name, surname, name, email, GenerateRandomUserPassword());  


    //    var registrationResponse = await CreateUserAsync(user);

    //    if(!registrationResponse.Succeeded)
    //        throw new Exception(registrationResponse.Message);

    //    await UpsertClaimAsync(registrationResponse.Value!, claim);

    //    return registrationResponse.Value!;
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

        if (foundClaim == null || foundClaim?.ToString() != newClaim.ToString())
            return false;

        var claimResponse = await this.UserManager.ReplaceClaimAsync(user, foundClaim, newClaim);
        
        return claimResponse.Succeeded;
    }




   



    //private async Task<Result<AppUser>> CreateUserAsync(AppUser user, string password)
    //{      
    //    var result = await this.UserManager.CreateAsync(user, password);
        
    //    if (!result.Succeeded)
    //        return Result<AppUser>.Error(string.Join(", ", result.Errors.Select(e => e.Description)));

    //    return Result<AppUser>.Success(user);
    //} 


    public async Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null)  
    {
        var user = await this.UserRepository.FindByEmailAsync(email);

        if (user != null)
            return Result<RegistrationResponse>.Error("Uporabnik že obstaja");

        var appUser = AppUser.Create(name, surname, nickname, email, email);
        var response = await this.UserRepository.AddAsync(appUser, password ?? GenerateUserPassword());     

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
