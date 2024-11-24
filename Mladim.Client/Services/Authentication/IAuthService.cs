using Mladim.Application.Models;
using Mladim.Client.ViewModels;
using Mladim.Domain.Models;
using System.Security.Claims;

namespace Mladim.Client.Services.Authentication;

public interface IAuthService
{
    Task<Result> TryChangePasswordAsync(string userId, string oldPassword, string password);
    Task<string?> GetUserIdentityAsync();
    Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser);
    Task<bool> IsUserPolicySatisfied(string organizationId, string policyName);

    Task LogoutAsync();  

    Task<Result<AuthResponse>> ConfirmRegistrationAsync(string name, string email, string emailToken, string password, bool mladim1ka);
    Task<Result> ResetPasswordAsync(UserEmail email);
    Task<Result> ConfirmResetPasswordAsync(NewPasswordUser user);
}
