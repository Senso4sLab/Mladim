using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public abstract class Group: BaseEntity<int>
{    
    //public GroupType GroupType { get; set; }
    public string Name { get; set; } = string.Empty;
    //public bool IsActive { get; set; } = true;
    public string Description { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new(); 
}
