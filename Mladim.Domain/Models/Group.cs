﻿
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mladim.Domain.Models;

public class Group: NamedEntity
{    
    public string Description { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new();
    public bool IsActive { get; set; }
    public int OrganizationId { get; set; }   

    protected Group() { }   

    protected Group(int id) =>
       this.Id = id;

    protected Group(string name, string description, IEnumerable<Member> members, int organizationId)
    {
        FullName = name;
        Description = description;
        Members = members.ToList();
        IsActive = true;
        OrganizationId = organizationId;
    }

    public static Group Create(GroupType groupType, int id) =>
           groupType switch
           {
               GroupType.Project => new ProjectGroup(id),
               GroupType.Activity => new ActivityGroup(id),              
               _ => throw new NotImplementedException()
           };

    public static Group Create(GroupType groupType, string name, string description, IEnumerable<int>memberIds, int organizationId) =>    
        groupType switch
        {
            GroupType.Project => new ProjectGroup(name, description, memberIds.Select(id => Member.Create(MemberType.StaffMember, id)), organizationId),
            GroupType.Activity => new ActivityGroup(name, description, memberIds.Select(id => Member.Create(MemberType.Participant,id)), organizationId),            
            _ => throw new NotImplementedException()
        } ;    
}
