using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class GroupRepository : GenericRepository<Group> , IGroupRepository
{   
    public GroupRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Group?> GetGroupDetailsAsync(int groupId, bool tracking = true)
    {
        var group = this.DbSet.Include(g => g.Members.Select(m => m as NamedEntity));

        return tracking ? await group.AsTracking().FirstOrDefaultAsync(g => g.Id == groupId)
            : await group.AsNoTracking().FirstOrDefaultAsync(a => a.Id == groupId);
    }

    
}
