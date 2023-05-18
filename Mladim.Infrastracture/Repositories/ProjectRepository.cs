using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class ProjectRepository : IProjectRepository
{
    private ApplicationDbContext Context { get; }
   
    public ProjectRepository(ApplicationDbContext context)
	{
        Context = context;      
    }   
   

    public async Task<Project?> AddAsync(Project entity)
    {
        var project = await this.Context.Projects.AddAsync(entity);
        return project?.Entity;
    }

    public async Task<Project?> GetByIdAsync(int id, params Expression<Func<Project, object>>[] includes)
    {
        var queryable = Context.Projects.AsQueryable();

        if (includes != null)
            queryable = includes.Aggregate(queryable, (first, other) => first.Include(other));

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<IEnumerable<Project>> GetAllAsync(Expression<Func<Project, bool>> predicate)
    {
        return await Context.Projects.Where(predicate).AsNoTracking().ToListAsync();
    }

    public void Remove(Project Project) =>
        this.Context.Projects.Remove(Project);

    public Task<bool> AnyAsync(Expression<Func<Project, bool>> predicate) =>
        this.Context.Projects.AnyAsync(predicate);

    public void Update(Project Project) =>
        this.Context.Projects.Update(Project);


}
