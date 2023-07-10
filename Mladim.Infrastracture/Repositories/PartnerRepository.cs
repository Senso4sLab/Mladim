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

public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
{
    public PartnerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Member>> GetPartnersAsync(Expression<Func<Partner, bool>> predicate, bool withDetails)
    {
        var smQuerable = DbSet.Where(predicate);

        if (!withDetails)
            return await smQuerable.Select(sm => sm as Member).ToListAsync();

        return await smQuerable.ToListAsync();
    }


}
