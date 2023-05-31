﻿using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
{
    
    public OrganizationRepository(ApplicationDbContext context) : base(context)
    {
        

        
    }

    public async override Task<Organization?> FirstOrDefaultAsync(Expression<Func<Organization, bool>> predicate, bool tracking = true)
    {

        return await DbSet.Include(o => o.SocialMediaUrls).FirstOrDefaultAsync(predicate);
       
    }


}

   

