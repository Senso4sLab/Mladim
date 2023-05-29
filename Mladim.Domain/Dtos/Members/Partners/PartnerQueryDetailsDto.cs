using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class PartnerQueryDetailsDto :MemberBase
{
  
    public string Description { get; set; }
    public string WebpageUrl { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
}
