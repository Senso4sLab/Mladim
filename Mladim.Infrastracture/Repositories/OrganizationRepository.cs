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

public class OrganizationRepository : IOrganizationRepository
{
    private ApplicationDbContext Context { get; }
    public OrganizationRepository(ApplicationDbContext context)
    {
        this.Context = context;
    }

    public async Task<Organization?> AddAsync(Organization entry)
    {
        var organization = await this.Context.Organizations
            .AddAsync(entry);
        
        return organization?.Entity;
    }

    public ValueTask<Organization?> GetByIdAsync(int id) =>
        Context.Organizations.FindAsync(id);


    public async Task<IEnumerable<Organization>>GetAllAsync(Expression<Func<Organization, bool>> predicate)
    {
        return await Context.Organizations.Where(predicate).AsNoTracking().ToListAsync();
    }

    public void Remove(Organization organization) =>          
        this.Context.Organizations.Remove(organization);

    public Task<bool> AnyAsync(Expression<Func<Organization, bool>> predicate) =>    
        this.Context.Organizations.AnyAsync(predicate);

    public void Update(Organization organization) =>
        this.Context.Organizations.Update(organization);
    
}

   

