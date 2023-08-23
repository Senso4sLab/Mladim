using Mladim.Application.Models;
using Mladim.Client.ViewModels;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.Authentication;

public interface IAuthService
{
    Task<Result<AuthResponse>> ChangePasswordAsync(UserPassword userPassword);
    Task<string?> GetUserIdentityAsync();
    Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser);
    Task LogoutAsync();
}
