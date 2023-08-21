using Microsoft.EntityFrameworkCore;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;
using Mladim.Application.Contracts.Persistence;

namespace Mladim.Infrastracture.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbSet<T> DbSet { get; }   
  
    public GenericRepository(ApplicationDbContext context) => 
        this.DbSet = context.Set<T>();
   
    public virtual async Task<T> AddAsync(T entity)
    {
        var response = await DbSet.AddAsync(entity);
        return response.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities) =>    
        await DbSet.AddRangeAsync(entities);    

    public virtual bool Remove(T entity) =>
        this.DbSet.Remove(entity).Entity != null;    

    public virtual void RemoveRange(IEnumerable<T> entities) =>     
        this.DbSet.RemoveRange(entities);    

    public virtual T Update(T entity) =>
       this.DbSet.Update(entity).Entity;
    
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) =>
        await DbSet.AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) =>
       await DbSet.CountAsync(predicate);

    public virtual async Task<T?> FindAsync(object id) =>
       await DbSet.FindAsync(id);

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
    {
        if(tracking)
            return await DbSet.FirstOrDefaultAsync(predicate);
        else
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }   

    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
    {
        if (tracking)
            return await DbSet.Where(predicate).ToListAsync();
        else
            return await DbSet.Where(predicate).AsNoTracking().ToListAsync();
    }
}
