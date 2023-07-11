using Mladim.Domain.Contracts;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.Persistence;

public interface IPartnerRepository : IGenericRepository<Partner>
{
    Task<IEnumerable<IFullName>> GetPartnersByFullNameAsync(Expression<Func<Partner, bool>> predicate);
}
