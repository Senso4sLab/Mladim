using Mladim.Domain.Dtos;

namespace Mladim.Client.ViewModels;

public class ProjectVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? WebpageUrl { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public List<StaffMemberProjectVM> Staff { get; set; } = new();
    public List<GroupVM> Groups { get; set; } = new();
    public List<PartnerVM> Partners { get; set; } = new();
}
