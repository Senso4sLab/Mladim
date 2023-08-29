using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;

namespace Mladim.Infrastracture.Repositories;

public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
{
    
    public AppUserRepository(ApplicationDbContext context) : base(context)
    {
        
    }

    public async Task<bool> IsUserInOrganizationAsync(string userId, int organizationId)
    {
        return await this.DbSet.Where(u => u.Id == userId).Include(u => u.Organizations).AnyAsync(u => u.Organizations.Any(o => o.Id == organizationId));
    }
}

