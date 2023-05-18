using Mladim.Application.Contract;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class GroupRepository :IGroupRepository
{
    private ApplicationDbContext Context { get; }
    public GroupRepository(ApplicationDbContext context) 
    {
        Context = context;
    }

  
}
