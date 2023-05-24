using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mladim.Application.Contracts.Identity;
using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models.Result;
using Mladim.Domain.Roles;
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
   
    public async Task<Result<AuthResponse>> LoginAsync(AuthRequest request)
    {
        var user = await this.UserManager.FindByEmailAsync(request.Email);

        if (user == null)
            return new Result<AuthResponse>("Vnešeni podatki so napačni");       

        bool passwordCorrect = await this.UserManager.CheckPasswordAsync(user, request.Password);           

        if (!await this.UserManager.CheckPasswordAsync(user, request.Password))
            return new Result<AuthResponse>("Vnešeni podatki so napačni");

        var authResponse =  new AuthResponse
        {
            Id     = user.Id,
            Name   = user.Name,
            Email  = user.Email!,
            Token  = await CreateTokenAsync(user),
        };        

        return new Result<AuthResponse>(authResponse,true);          
    }


    private async Task<string> CreateTokenAsync(AppUser user)
    {
        List<Claim> tokenClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        foreach (var role in await UserManager.GetRolesAsync(user))
            tokenClaims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));

        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                claims: tokenClaims,
                signingCredentials: credential
                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<Result<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
    {
        var user = await this.UserManager.FindByEmailAsync(request.Email);

        if (user != null)
            return new Result<RegistrationResponse>("Uporabnik že obstaja");       

        var appUser = new AppUser
        {   
            Name = request.Name,
            Surname = request.Surname,
            Nickname = request.Nickname,
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await this.UserManager.CreateAsync(appUser, request.Password);

        if (!result.Succeeded)
            return new Result<RegistrationResponse>(string.Join(",", result.Errors.Select(e => e.Description)));      

        result = await this.UserManager.AddToRoleAsync(appUser, ApplicationRoles.Worker);

        if (!result.Succeeded)
            return new Result<RegistrationResponse>(string.Join(",", result.Errors.Select(e => e.Description)));

        var registrationResponse = new RegistrationResponse
        {
            UserId = appUser.Id,
        };

        return new Result<RegistrationResponse>(registrationResponse, true);      
    }
}
