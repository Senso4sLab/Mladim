using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public abstract class Group: BaseEntity<int>
{       
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new(); 

    protected Group() { }

    protected Group(string name, string description, IEnumerable<Member> members)
    {
        this.Name = name;
        this.Description = description;
        this.Members = members.ToList();
    }

    public static Group Create(MemberType groupType, string name, string description, IEnumerable<int>memberIds) =>    
        groupType switch
        {
            MemberType.StaffMember => new ProjectGroup(name, description, memberIds.Select(id => StaffMember.Create(id))),
            MemberType.Participant => new ActivityGroup(name, description, memberIds.Select(id => Participant.Create(id))),
            _ => throw new NotImplementedException()
        } ;

    
}
