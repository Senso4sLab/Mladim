using Microsoft.EntityFrameworkCore;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;
using Mladim.Infrastracture.Extensions;
using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;
using Mladim.Application.Contracts.Persistence;

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

    public virtual async Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, bool>>> predicates,        
        bool tracking = true)
    {
        return await ApplyWhereConditionsToSequence(predicates, tracking).ToListAsync();         
    }

    private IQueryable<T> ApplyWhereConditionsToSequence(IEnumerable<Expression<Func<T, bool>>> predicates, bool tracking = true)
    {
        var dbSetQ = predicates.Aggregate(this.DbSet.AsQueryable(), (sequence, predicate) => sequence.Where(predicate));

        if (!tracking)
            dbSetQ = dbSetQ.AsNoTracking();

        return dbSetQ;
    }



    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(IEnumerable<Expression<Func<T, bool>>> predicates,
       Expression<Func<T, TResult>> selector, bool tracking = true)
    {
        var dbSet = ApplyWhereConditionsToSequence(predicates, tracking);
        return await dbSet.Select(selector).ToListAsync();
    }

   



   

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
    {
        if(tracking)
            return await DbSet.FirstOrDefaultAsync(predicate);
        else
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    
}
