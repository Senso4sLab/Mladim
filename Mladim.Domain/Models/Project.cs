using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string WebpageUrl { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public List<Activity> Activities { get; set; } = new();
    public List<MemberProject> ProjectMembers { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

}
