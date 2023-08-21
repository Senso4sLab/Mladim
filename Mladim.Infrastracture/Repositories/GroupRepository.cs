using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

public class GroupRepository : GenericRepository<Group> , IGroupRepository
{   
    public GroupRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Group?> GetGroupDetailsAsync(int groupId, bool tracking = true)
    {
        try
        {
            var group = this.DbSet.Include(g => g.Members);
            
            return tracking ? await group.AsTracking().FirstOrDefaultAsync(g => g.Id == groupId)
                : await group.AsNoTracking().FirstOrDefaultAsync(a => a.Id == groupId);
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public async Task<IEnumerable<TResult>> GetAllGroupsAsync<TResult>(Expression<Func<TResult, bool>> predicate) where TResult : Group
    {
        return await DbSet.OfType<TResult>().Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<int> NumberOfMemberInGroupAsync<TResult>(Expression<Func<TResult, bool>> predicate) where TResult : Group
    {
        return await DbSet.OfType<TResult>().Where(predicate)
            .Include(o => o.Members)
            .SumAsync(o => o.Members.Count);
    }





}
