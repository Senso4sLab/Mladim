using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static FluentValidation.Validators.PredicateValidator<T, TProperty>;

namespace Mladim.Infrastracture.Repositories;

public class ProjectRepository : GenericRepository<Project>,  IProjectRepository
{   
    public ProjectRepository(ApplicationDbContext context) : base(context)
	{
       
    }

    public async Task<Project?> FirstOrDefaultWithoutIncludeAsync(Expression<Func<Project, bool>> predicate, bool tracking = true)
    {
        var dbSetQ = this.DbSet.AsQueryable();

        if (!tracking)
            dbSetQ = dbSetQ.AsNoTracking();

        return await dbSetQ.FirstOrDefaultAsync(predicate);
    }


    public async Task<Project?> GetProjectDetailsAsync(int projectId, bool tracking = true)
    {
        var projectDbSet = this.DbSet.Include("staff")
            .Include(p => p.Partners)
            .Include(p => p.Groups);

        return tracking ? await projectDbSet.AsTracking().FirstOrDefaultAsync()
            : await projectDbSet.AsNoTracking().FirstOrDefaultAsync();        
    }

    public override async Task<Project?> FirstOrDefaultAsync(Expression<Func<Project, bool>> predicate, bool tracking = true)
    {
        var dbSetQ = this.DbSet.AsQueryable();            

        if (!tracking)
            dbSetQ = dbSetQ.AsNoTracking();

        return await dbSetQ
            .Include(p => p.Staff)
                .ThenInclude(sp => sp.StaffMember)
            .Include(p => p.Partners)
            .Include(p => p.Groups)               
            .FirstOrDefaultAsync(predicate);       
    }

    public async Task<IEnumerable<ActivityWithProjectName>> GetActivitiesWithProjectNameByOrganizationId(int organizationId)
    {
        var result = await this.DbSet.Where(p => p.OrganizationId == organizationId)
            .SelectMany(p => p.Activities.Select(a => ActivityWithProjectName.Create(a, p.Name)))
            .ToListAsync();

        return result;
    }

    
}
