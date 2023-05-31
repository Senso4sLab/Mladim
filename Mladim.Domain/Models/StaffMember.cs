using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class StaffMember : Member
{    
    public string Email { get; set; }
    public int? YearOfBirth { get; set; }
    public bool IsRegistered { get; set; }
    public List<StaffMemberActivity> StaffActivities { get; set; } = new();
    public List<StaffMemberProject> StaffProjects { get; set; } = new();    

}