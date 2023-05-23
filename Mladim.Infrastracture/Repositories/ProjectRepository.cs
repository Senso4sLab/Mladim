﻿using Microsoft.EntityFrameworkCore;
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
}
