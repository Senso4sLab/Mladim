﻿using Azure.Core;
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


namespace Mladim.Infrastracture.Repositories;

public class ProjectRepository : GenericRepository<Project>,  IProjectRepository
{   
    public ProjectRepository(ApplicationDbContext context) : base(context)
	{
       
    }

    //public async Task<Project?> FirstOrDefaultWithoutIncludeAsync(Expression<Func<Project, bool>> predicate, bool tracking = true)
    //{
    //    var dbSetQ = this.DbSet.AsQueryable();

    //    if (!tracking)
    //        dbSetQ = dbSetQ.AsNoTracking();

    //    return await dbSetQ.FirstOrDefaultAsync(predicate);
    //}


    public async Task<Project?> GetProjectDetailsAsync(int projectId, bool tracking = true)
    {
        var projectDbSet = this.DbSet
            .Include(p => p.Staff)
            .Include(p => p.Partners)
            .Include(p => p.Groups);

        return tracking ? await projectDbSet.FirstOrDefaultAsync(p => p.Id == projectId)
            : await projectDbSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == projectId);        
    }

    public async Task<IEnumerable<Project>> GetProjectsWithStaffMemberWithAsync(int organizationId, string email)
    {
        var projectDbSet = this.DbSet
           .Include(p => p.Staff).AsNoTracking();

        return await projectDbSet.Where(p => p.OrganizationId == organizationId && p.Staff.Any(s => s.StaffMember.Email == email)).ToListAsync();

    }
    
   
}
