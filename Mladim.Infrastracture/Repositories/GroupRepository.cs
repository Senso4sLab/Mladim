using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class GroupRepository : GenericRepository<Group>
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {
    }
}
