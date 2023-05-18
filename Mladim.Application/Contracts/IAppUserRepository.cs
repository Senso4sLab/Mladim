using Mladim.Domain.IdentityModels;
using System.Linq.Expressions;

namespace Mladim.Application.Contract;

public interface IAppUserRepository
{
    Task<bool> AnyAsync(Expression<Func<AppUser, bool>> predicate);
}