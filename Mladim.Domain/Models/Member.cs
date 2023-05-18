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

    public int Year { get; set; }
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<Group> Groups { get; set; } = new();

    public List<MemberProject> MemberProjects { get; set; } = new();
    public List<MemberActivity> MemberActivities { get; set; } = new();
}

