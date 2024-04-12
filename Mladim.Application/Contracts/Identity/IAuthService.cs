
using Microsoft.AspNetCore.Identity;
using Mladim.Application.Models;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Identity;

public interface IAuthService
{
    Task<bool> AddClaimAsync(AppUser user, Claim newClaim);
    Task<bool> AddUserRoleAsync(string userId, string role);
    Task<string> EmailTokenAsync(AppUser appUser);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);

    //Task<bool> ExistClaimValueAsync(AppUser user, ApplicationClaim claimValue);    
    Task<Result<AuthResponse>> LoginAsync(string email, string password);
    Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null);
    Task<Result<AuthResponse>> RegisterConfirmationAsync(string email, string emailToken, string password);
    Task<bool> ReplaceClaimAsync(AppUser user, Claim newClaim);
    Task<bool> ResetPasswordAsync(AppUser user, string token, string password);
}
