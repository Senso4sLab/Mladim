using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;

namespace Mladim.Infrastracture.Repositories;

public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
{
    private ApplicationDbContext Context { get; }
    public AppUserRepository(ApplicationDbContext context) : base(context)
    {
        
    } 

}

