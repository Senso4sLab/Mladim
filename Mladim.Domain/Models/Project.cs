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
    private Project() {}
    public List<Activity> Activities { get; set; } = new();
    public List<StaffMemberProject> Staff { get; set; } = new();
    public List<ProjectGroup> Groups { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();
  

    //public bool Exists(Partner partner) =>
    //    this.Partners.Any(p => p == partner);
    //public bool Exists(Group group) =>
    //    this.Groups.Any(g => g == group);
    //public bool Exists(StaffMember other) =>
    //     this.Staff.Any(smp => smp.StaffMember == other);
    public void Add(Partner partners) =>
        this.Partners.Add(partners);     
    public void Add(ProjectGroup group) =>
        this.Groups.Add(group);
    public void Add(StaffMemberRole sm) =>
       this.Staff.Add(StaffMemberProject.Create(sm.StaffMember,this, sm.IsLead));
    //public void RemoveAll(IEnumerable<Partner> partners)
    //{
    //    foreach(var partner in partners)
    //        this.Partners.Remove(partner);
    //}
    //public void RemoveAll(IEnumerable<ProjectGroup> groups)
    //{
    //    foreach (var group in groups)
    //        this.Groups.Remove(group);
    //}
    //public void RemoveAll(IEnumerable<StaffMemberProject> staffMemberProjects)
    //{
    //    foreach (var staffmemberProject in staffMemberProjects)
    //        this.Staff.Remove(staffmemberProject);        
    //}

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

}
