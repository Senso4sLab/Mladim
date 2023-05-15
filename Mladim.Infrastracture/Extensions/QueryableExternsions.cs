using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mladim.Infrastracture.Extensions;

public static class QueryableExternsions
{
    public static IQueryable<T> AddIncludes<T>(this IQueryable<T> sequence, IEnumerable<Expression<Func<T, string>>> includes) where T : class
    {      
        if(includes != null)
            foreach (var include in includes)
                sequence.Include(include);

        return sequence;
    }
}
