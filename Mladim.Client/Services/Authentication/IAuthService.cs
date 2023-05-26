using Mladim.Application.Models;
using Mladim.Client.ViewModels;
using Mladim.Domain.Models;

namespace Mladim.Client.Services.Authentication;

public interface IAuthService
{
    Task<string?> GetUserIdentityAsync();
    Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser);
    Task LogoutAsync();
}
