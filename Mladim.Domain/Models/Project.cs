using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;


public class BaseProjectAttibutes
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? WebpageUrl { get; private set; }
    private BaseProjectAttibutes()
    {

    }

    private BaseProjectAttibutes(string name, string description, string? webpageUrl = null)
    {
        this.Name = name;
        this.Description = description;
        this.WebpageUrl = webpageUrl;
    }


    public static  BaseProjectAttibutes Create(string name, string description, string? webpageUrl = null) =>
        new BaseProjectAttibutes(name, description, webpageUrl);


}


public class Project : BaseEntity<int>
{   
    public BaseProjectAttibutes BaseProjectAttibutes { get; private set; }    
    public DateTimeRange DateTimeRange { get; private set; }
    private Project()
    {
        
    }
    public Project(int id, DateTimeRange dateTimeRange, BaseProjectAttibutes baseProjectAttibutes) 
    {
        this.Id = id;
        this.DateTimeRange = dateTimeRange;
        this.BaseProjectAttibutes = baseProjectAttibutes;
    }

    public void SetBaseAttributes(string name, string description, string? webpageUrl = null) =>
        this.BaseProjectAttibutes = BaseProjectAttibutes.Create(name, description, webpageUrl);  

    public void PeriodOfImplementation(DateTime start, DateTime end) => 
        this.DateTimeRange = DateTimeRange.Create(start, end);

    public List<Activity> Activities { get; set; } = new();

    public IEnumerable<StaffMember> LeadStaff =>
        staff.Where(s => s.IsLead).Select(s => s.StaffMember);

    public IEnumerable<StaffMember> Staff =>
        staff.Where(s => !s.IsLead).Select(s => s.StaffMember);

    private List<StaffMemberProject> staff { get; set; } = new();

    public void AddStaffMember(int staffMemberId, bool lead) =>
        staff.Add(StaffMemberProject.Create(staffMemberId, this.Id, lead));   


    public List<ProjectGroup> Groups { get; set; } = new();

    public List<Partner> Partners { get; set; } = new();

    public bool ExistsPartner(int partnerId) =>
        this.Partners.Any(p => p.Id == partnerId);
    public void AddPartners(IEnumerable<Partner> partners) =>
        this.Partners.AddRange(partners);
    public void RemovePartnersIfNotExistIn(IEnumerable<BaseEntity<int>> partnerIds)
    {
        var partners = this.Partners.Where(p => !partnerIds.Any(other => other == p)).ToList();
        this.RemoveAll(partners);       
    }

    public bool ExistsGroup(int groupId) =>
        this.Groups.Any(g => g.Id == groupId);
    public void AddGroup(IEnumerable<ProjectGroup> group) =>
        this.Groups.AddRange(group);

    public void RemoveGroupsIfNotExistIn(IEnumerable<BaseEntity<int>> groupsIds)
    {
        var groups = this.Groups.Where(g => !groupsIds.Any(other => other == g)).ToList();
        this.RemoveAll(groups);
    }

    private void RemoveAll(IEnumerable<Partner> partners)
    {
        foreach(var partner in partners)
            this.Partners.Remove(partner);
    }

    private void RemoveAll(IEnumerable<ProjectGroup> groups)
    {
        foreach (var group in groups)
            this.Groups.Remove(group);
    }





    public int OrganizationId { get; set; }
    public Organization Organization { get; set; } = default!;

}
