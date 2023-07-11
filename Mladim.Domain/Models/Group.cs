using Mladim.Domain.Contracts;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mladim.Domain.Models;

public abstract class Group: BaseEntity<int>, IFullName
{       
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new();
    public int OrganizationId { get; set; }   

    protected Group() { }
    
    protected Group(string name, string description, IEnumerable<Member> members) => 
        (FullName, Description, members) = (name, description, members.ToList());

    protected Group(int id) =>
       this.Id = id;

    public static Group Create(GroupType groupType, int id) =>
           groupType switch
           {
               GroupType.Project => new ProjectGroup(id),
               GroupType.Activity => new ActivityGroup(id),
               _ => throw new NotImplementedException()
           };

    public static Group Create(GroupType groupType, string name, string description, IEnumerable<int>memberIds) =>    
        groupType switch
        {
            GroupType.Project => new ProjectGroup(name, description, memberIds.Select(id => Member.Create(GroupType.StaffMember, id))),
            GroupType.Activity => new ActivityGroup(name, description, memberIds.Select(id => Member.Create(GroupType.Participant,id))),
            _ => throw new NotImplementedException()
        } ;    
}
