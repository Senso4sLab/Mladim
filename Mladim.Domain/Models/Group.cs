using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Group
{
    public int Id { get; set; }
    public GroupType GroupType { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public string Description { get; set; }
    public List<Member> Members { get; set; }
    public List<OrganizationGroup> OrganizationGroups { get; set; }
   
}
