using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IAppUserRepository
{
    Task<Result<AppUser>> CreateAsync(AppUser user, string password);
    Task<string> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByIdAsync(string userId);
    Task<bool> ExistUserAsync(string userId);
}