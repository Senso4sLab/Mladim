namespace Mladim.Client.ViewModels;

public class PartnerVM
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;

}
