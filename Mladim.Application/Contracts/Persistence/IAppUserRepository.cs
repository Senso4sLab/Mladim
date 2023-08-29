using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IAppUserRepository : IGenericRepository<AppUser>
{
    Task<Result<AppUser>> AddAsync(AppUser user, string password);
    Task<string> ChangePasswordAsync(string userId, string password);
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByIdAsync(string userId);
    Task<bool> IsUserInOrganizationAsync(string userId, int organizationId);
}