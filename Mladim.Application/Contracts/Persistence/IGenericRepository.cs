﻿using Microsoft.EntityFrameworkCore.Query;
using Mladim.Domain.Models;
using System.Linq.Expressions;

namespace Mladim.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
   

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);
    T Update(T entity);
    bool Remove(T entity);
    void Remove(IEnumerable<T> entities);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, bool tracking = true);    
    Task<IEnumerable<T>> GetAllAsync(IEnumerable<Expression<Func<T, bool>>> predicates, bool tracking = true);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(IEnumerable<Expression<Func<T, bool>>> predicates, Expression<Func<T, TResult>> selector, bool tracking = true);
}