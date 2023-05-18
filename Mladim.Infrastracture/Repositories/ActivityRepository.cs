using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class ActivityRepository : IActivityRepository
{
    private ApplicationDbContext Context { get; }

    public ActivityRepository(ApplicationDbContext context)
    {
        Context = context;
    }


    public async Task<Activity?> AddAsync(Activity entity)
    {
        var Activity = await this.Context.Activities.AddAsync(entity);
        return Activity?.Entity;
    }

    public async Task<Activity?> GetByIdAsync(int id, params Expression<Func<Activity, object>>[] includes)
    {
        var queryable = Context.Activities.AsQueryable();

        if (includes != null)
            queryable = includes.Aggregate(queryable, (first, other) => first.Include(other));

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<IEnumerable<Activity>> GetAllAsync(Expression<Func<Activity, bool>> predicate)
    {
        return await Context.Activities.Where(predicate).AsNoTracking().ToListAsync();
    }

    public void Remove(Activity Activity) =>
        this.Context.Activities.Remove(Activity);

    public Task<bool> AnyAsync(Expression<Func<Activity, bool>> predicate) =>
        this.Context.Activities.AnyAsync(predicate);

    public void Update(Activity Activity) =>
        this.Context.Activities.Update(Activity);

}

