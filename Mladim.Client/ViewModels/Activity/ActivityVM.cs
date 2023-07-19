using Mladim.Client.ViewModels.Activity;
using Mladim.Client.ViewModels.Project;
using Mladim.Domain.Dtos;
using Mladim.Domain.Enums;
using MudBlazor;

namespace Mladim.Client.ViewModels;

public class ActivityVM
{
    public int Id { get; set; }
    public ActivityAttributesVM Attributes { get; private set; } = new ActivityAttributesVM();

    public DateRange DateRange { get; set; } = new DateRange();
    public TimeSpan? StartTime { get; set; } = TimeSpan.Zero;
    public TimeSpan? EndTime { get; set; } = TimeSpan.Zero;    

    public IEnumerable<NamedEntityVM> Staff { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Administration { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Groups { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Partners { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Participants { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<AnonymousParticipantsVM> AnonymousParticipantActivities { get; set; } = new List<AnonymousParticipantsVM>();
}
