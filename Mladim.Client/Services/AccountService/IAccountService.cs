using Mladim.Client.ViewModels;

namespace Mladim.Client.Services.AccountService;

public interface IAccountService
{
    Task<AppUserVM?> GetAccountByIdAsync(string userId);
    Task<bool> UpdateAccountAsync(AppUserVM appUser);
}

