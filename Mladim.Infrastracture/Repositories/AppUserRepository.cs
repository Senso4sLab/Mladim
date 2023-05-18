using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;

namespace Mladim.Infrastracture.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private ApplicationDbContext Context { get; }
    public AppUserRepository(ApplicationDbContext context)
    {
        Context = context;
    }


    public Task<bool> AnyAsync(Expression<Func<AppUser, bool>> predicate) =>
       this.Context.AppUsers.AnyAsync(predicate);

}

