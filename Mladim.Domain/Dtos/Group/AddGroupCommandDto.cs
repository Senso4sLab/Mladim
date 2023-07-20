using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Group;

public class AddGroupCommandDto
{   
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public GroupType GroupType { get; set; }
    public List<int> Members { get; set; } = new();
    public int OrganizationId { get; set; }
}
