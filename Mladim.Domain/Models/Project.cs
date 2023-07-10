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
    public ProjectAttibutes BaseProjectAttibutes { get; private set; }    
    public DateTimeRange DateTimeRange { get; private set; }
    private Project()
    {
        
    }
    private Project(DateTimeRange dateTimeRange, ProjectAttibutes baseProjectAttibutes) 
    {        
        this.DateTimeRange = dateTimeRange;
        this.BaseProjectAttibutes = baseProjectAttibutes;
    }


    public static Project Create(DateTime start, DateTime end, string name,
        string description, string? webpageurl, IEnumerable<Partner> partners,
        IEnumerable<StaffMemberRole> staffRole, IEnumerable<ProjectGroup> groups) =>
            new Project(DateTimeRange.Create(start, end), ProjectAttibutes.Create(name, description, webpageurl))
            {              
                Groups = groups.ToList(),
                Partners = partners.ToList(),
                Staff = staffRole.Select(sr => StaffMemberProject.Create(sr.StaffMember, 0, sr.IsLead)).ToList(),               
            };      
    

    public void SetBaseAttributes(string name, string description, string? webpageUrl = null) =>
        this.BaseProjectAttibutes = ProjectAttibutes.Create(name, description, webpageUrl);  

    public void PeriodOfImplementation(DateTime start, DateTime end) => 
        this.DateTimeRange = DateTimeRange.Create(start, end);

    public List<Activity> Activities { get; set; } = new();  

    public List<StaffMemberProject> Staff { get; set; } = new();
    public List<ProjectGroup> Groups { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();

    public void SetStaffMemberRole(StaffMemberRole smRole) => 
         Staff.FirstOrDefault(s => s.StaffMember == smRole.StaffMember)?
              .SetIsLead(smRole.IsLead);   

    public bool Exists(Partner partner) =>
        this.Partners.Any(p => p == partner);
    public bool Exists(Group group) =>
        this.Groups.Any(g => g == group);
    public bool Exists(StaffMember other) =>
         this.Staff.Any(smp => smp.StaffMember == other);
    public void AddPartners(IEnumerable<Partner> partners) =>
        this.Partners.AddRange(partners);     
    public void AddGroups(IEnumerable<ProjectGroup> group) =>
        this.Groups.AddRange(group);
    public void AddStaff(IEnumerable<StaffMemberRole> staff) =>
       this.Staff.AddRange(staff.Select(sm => StaffMemberProject.Create(sm.StaffMember,this.Id, sm.IsLead)));
    public void RemoveAll(IEnumerable<Partner> partners)
    {
        foreach(var partner in partners)
            this.Partners.Remove(partner);
    }
    public void RemoveAll(IEnumerable<ProjectGroup> groups)
    {
        foreach (var group in groups)
            this.Groups.Remove(group);
    }
    public void RemoveAll(IEnumerable<StaffMemberProject> staffMemberProjects)
    {
        foreach (var staffmemberProject in staffMemberProjects)
            this.Staff.Remove(staffmemberProject);        
    }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

}
