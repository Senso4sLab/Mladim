using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contracts.Persistence;
using Mladim.Domain.Contracts;
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
    public PartnerRepository(ApplicationDbContext context) : base(context) {}

    public async Task<IEnumerable<INameableEntity>> GetPartnersAsync(Expression<Func<Partner, bool>> predicate, bool isMemberAbbreviated) =>
         isMemberAbbreviated ? await DbSet.Where(predicate).Select(p => p as INameableEntity).AsNoTracking().ToListAsync() :
            await DbSet.Where(predicate).AsNoTracking().ToListAsync();
}
