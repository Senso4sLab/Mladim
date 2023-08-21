using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;

using Mladim.Domain.Dtos;
using Mladim.Domain.IdentityModels;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class StaffMemberRepository : GenericRepository<StaffMember>, IStaffMemberRepository
{    
    public StaffMemberRepository(ApplicationDbContext context) : base(context) {}

    public async Task<IEnumerable<NamedEntity>> GetStaffMembersAsync(Expression<Func<StaffMember, bool>> predicate, bool memberAbbreviated) =>
        memberAbbreviated ? await DbSet.Where(predicate).Select(p => p as NamedEntity).AsNoTracking().ToListAsync() :
            await DbSet.Where(predicate).AsNoTracking().ToListAsync();


    public async Task<IEnumerable<StaffMemberLeadQuery>> GetLeadStaffMembersAsync(int organizationId)
    {
        var leadStaff = await DbSet.Where(sm => sm.OrganizationId == organizationId && (sm.StaffProjects.Any(sp => sp.IsLead) 
            || sm.StaffActivities.Any(sp => sp.IsLead)))
            .Select(sm => StaffMemberLeadQuery.Create(sm.Id,$"{sm.Name} {sm.Surname}",  sm.StaffProjects.Where(sp => sp.IsLead).Select(sp => sp.ProjectId), sm.StaffActivities.Where(sp => sp.IsLead).Select(sa => sa.ActivityId)))
            .ToListAsync();

        return leadStaff;
    }
      

}
