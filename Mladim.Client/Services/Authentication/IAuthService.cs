using Mladim.Application.Models;
using Mladim.Client.ViewModels;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.Authentication;

public interface IAuthService
{
    Task<bool> TryChangePasswordAsync(string userId, string oldPassword, string password);
    Task<string?> GetUserIdentityAsync();
    Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser);
    Task LogoutAsync();
    //Task<Result<AuthResponse>> ConfirmRegistrationAsync(string userId, string password);

    Task<Result<AuthResponse>> ConfirmRegistrationAsync(string email, string emailToken, string password);
}
