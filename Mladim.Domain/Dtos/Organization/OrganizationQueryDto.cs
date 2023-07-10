using Mladim.Domain.Dtos.Attributes;

namespace Mladim.Domain.Dtos;

public class OrganizationQueryDto
{
    public int Id { get; set; }
    public OrganizationAttributesQueryDto Attributes { get; set; } = default!;   
    public SocialMediaUrlsQueryDto SocialMediaUrls { get; set; } = default!;
}