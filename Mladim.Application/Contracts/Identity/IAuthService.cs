using Mladim.Application.Models;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Security.Claims;

namespace Mladim.Application.Contracts.Identity;

public interface IAuthService
{
    Task<bool> AddClaimAsync(AppUser user, Claim newClaim);
    Task<bool> AddUserRoleAsync(string userId, string role);
    Task<Result> ChangePasswordrequestAsync(AppUser user, string oldPassword, string newPassword);
    Task<string> GenerateEmailTokenAsync(AppUser appUser);
    Task<string> GeneratePasswordResetTokenAsync(AppUser user);     
    Task<Result<AuthResponse>> LoginAsync(string email, string password);
    Task<Result<RegistrationResponse>> RegisterAsync(string name, string surname, string nickname, string email, string? password = null);
    Task<Result<AuthResponse>> RegisterConfirmationAsync(string name, string email, string emailToken, string password, bool mladim1ka);
    Task<bool> ReplaceClaimAsync(AppUser user, Claim newClaim);
    Task<bool> ResetPasswordAsync(AppUser user, string token, string password);
    Task<Result> ResetPasswordRequestAsync(AppUser user, string token, string password);
}
