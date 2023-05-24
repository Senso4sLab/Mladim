using Mladim.Client.Models;
using Mladim.Domain.Models.Login;
using Mladim.Domain.Models.Result;

namespace Mladim.Client.Services.Authentication;

public interface IAuthService
{
    Task<string?> GetUserIdentityAsync();
    Task<Result<AuthResponse>> LoginAsync(LoginUser loginUser);
    Task LogoutAsync();
}
