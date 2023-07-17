using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;


public class Project : BaseEntity<int>
{
    public ProjectAttibutes Attributes { get; private set; } = default!;
    public DateTimeRange TimeRange { get; private set; } = default!;   
    public List<Activity> Activities { get; set; } = new();
    public List<StaffMemberProject> Staff { get; set; } = new();
    public List<ProjectGroup> Groups { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();

    private Project() { }
   
    public void Add(ProjectGroup group) =>
        this.Groups.Add(group);    

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

}
