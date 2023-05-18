using Microsoft.EntityFrameworkCore;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;
using Mladim.Infrastracture.Extensions;
using Mladim.Application.Contract;
using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;

namespace Mladim.Infrastracture.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbSet<T> DbSet { get; }

    public GenericRepository(ApplicationDbContext context)
    {
        this.DbSet = context.Set<T>();
    }
    public virtual async Task<T> AddAsync(T entity)
    {
        var response = await DbSet.AddAsync(entity);
        return response.Entity;
    }

    public virtual async Task AddAsync(IEnumerable<T> entities)
    {
        await DbSet.AddRangeAsync(entities);        
    }

    public virtual bool Remove(T entity)
    {        
        return this.DbSet.Remove(entity).Entity != null;
    }

    public virtual void Remove(IEnumerable<T> entities)
    { 
        this.DbSet.RemoveRange(entities);
    }

    public virtual T Update(T entity) =>
       this.DbSet.Update(entity).Entity;
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
        await DbSet.AnyAsync(predicate);

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var queryable = DbSet.AsQueryable<T>().Where(predicate);
      
        if (includes != null)
            queryable = includes.Aggregate(queryable, (sequence, element) => sequence.Include(element));   

        return await queryable.ToListAsync() ?? Enumerable.Empty<T>();       
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var queryable = DbSet.AsQueryable<T>();

        if (includes != null)
            queryable = includes.Aggregate(queryable, (sequence, element) => sequence.Include(element));

        return await queryable.FirstOrDefaultAsync(predicate);
    }
}
