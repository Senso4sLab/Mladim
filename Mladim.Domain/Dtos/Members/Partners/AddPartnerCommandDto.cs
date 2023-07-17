namespace Mladim.Client.ViewModels;

public class AddPartnerCommandDto
{
    public int OrganizationId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
}
