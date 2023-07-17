using Mladim.Domain.Dtos.Attributes;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class AddOrganizationCommandDto
{
    public string AppUserId { get; set; } = string.Empty;
    public OrganizationAttributesCommandDto Attributes { get; set; } = default!;    
    public SocialMediaUrlsCommandDto SocialMediaUrls { get; set; } = default!;
}
