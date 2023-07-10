namespace Mladim.Client.ViewModels;

public class UpdatePartnerCommandDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
}
