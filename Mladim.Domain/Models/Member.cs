using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    //public int Year { get; set; }
    public bool IsActive { get; set; } = true;
    //public List<OrganizationMember> OrganizationMembers{ get; set; } = new();   

}

