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
    private Organization() { } 
    private Organization(OrganizationAttributes attributes, SocialMediaUrls urls) =>
        (Attributes, SocialMediaUrls) = (attributes, urls);   
    public static Organization Create(OrganizationAttributes attributes, SocialMediaUrls urls) =>
        new Organization(attributes, urls);
    public List<Project> Projects { get; set; } = new();  
    public List<AppUser> AppUsers { get; set; } = new();
}
