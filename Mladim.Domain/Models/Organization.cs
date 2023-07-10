using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using Mladim.Domain.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Organization : BaseEntity<int>
{
    public OrganizationAttributes Attributes { get; private set; } = default!;
    public SocialMediaUrls SocialMediaUrls { get; private set; } = default!;


    private Organization()
    {
            
    }

    public void SetAttributes(OrganizationAttributes attributes) => 
        this.Attributes = attributes;

    public void SetMedialUrls(SocialMediaUrls urls) =>
        this.SocialMediaUrls = urls;

    private Organization(OrganizationAttributes organizationAttributes, SocialMediaUrls socialMediaUrls)
    {
        this.Attributes = organizationAttributes;
        this.SocialMediaUrls = socialMediaUrls;
    }

    public static Organization Create(OrganizationAttributes organizationAttributes, SocialMediaUrls socialMediaUrls) =>
        new Organization(organizationAttributes, socialMediaUrls);


    public List<OrganizationPartner> Partners { get; set; } = new();
           
    public List<OrganizationMember> Members { get; set; } = new();

    public void Add(Member member) =>
        this.Members.Add(OrganizationMember.Create(member));

    public void Add(Partner partner) =>
        this.Partners.Add(OrganizationPartner.Create(partner));

    public void Add(Group group) =>
        this.Groups.Add(OrganizationGroup.Create(group));

    public List<OrganizationGroup> Groups { get; set; } = new();    

    public List<Project> Projects { get; set; } = new();  
    public List<AppUser> AppUsers { get; set; } = new();

}
