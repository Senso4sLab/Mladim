using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(ApplicationDbContext context) : base(context)
    {        
    }

    public async Task<bool> IsUserInOrganizationAsync(string userId, int organizationId)
    {
        return await this.DbSet.Where(o => o.Id == organizationId)
            .Include(o => o.AppUsers)
            .AnyAsync(o => o.AppUsers.Any(u => u.Id == userId));
    }

    public async Task<IEnumerable<Organization>> GetAllWithAppUser(string userId)
    {
        return await this.DbSet
            .Include(o => o.AppUsers)            
            .Where(o => o.AppUsers.Any(u => u.Id == userId))
            .AsNoTracking()
            .ToListAsync();
    }

}



