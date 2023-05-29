using Microsoft.EntityFrameworkCore;
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

public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{


    public ActivityRepository(ApplicationDbContext context) : base(context)
    {

    }



    public async override Task<Activity?> FirstOrDefaultAsync(Expression<Func<Activity, bool>> predicate, bool tracking = true)
    {
        return await this.DbSet
             .Include(a => a.Groups)
             .Include(a => a.AnonymousParticipants)
                 .ThenInclude(ap => ap.AnonymousParticipant)
             .Include(a => a.Participants)
             .Include(a => a.Partners)
             .Include(a => a.Staff)
                 .ThenInclude(sa => sa.StaffMember)
             .FirstOrDefaultAsync(predicate);
    }


   


}

