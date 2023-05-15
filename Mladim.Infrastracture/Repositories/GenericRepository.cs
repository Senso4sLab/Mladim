using Microsoft.EntityFrameworkCore;
using Mladim.Infrastracture.Persistance;
using System.Linq.Expressions;
using Mladim.Infrastracture.Extensions;
using Mladim.Application.Contract;

namespace Mladim.Infrastracture.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbSet<T> DbSet { get; }

    public GenericRepository(ApplicationDbContext context)
    {
        this.DbSet = context.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
        var response = await DbSet.AddAsync(entity);
        return response.Entity;
    }

    public bool Remove(T entity) =>
        this.DbSet.Remove(entity).Entity != null;

    public void Remove(IEnumerable<T> entities) =>
        this.DbSet.RemoveRange(entities);

    public T Update(T entity) =>
       this.DbSet.Update(entity).Entity;

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null)
    {
        return await DbSet.Where(predicate).AddIncludes(includes).ToListAsync() ?? Enumerable.Empty<T>();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, IEnumerable<Expression<Func<T, string>>> includes = null)
    {
        return await DbSet.AsQueryable<T>().AddIncludes(includes).FirstOrDefaultAsync(predicate);
    }
}
