using Mladim.Client.ViewModels.AttachedFile;
using Mladim.Client.ViewModels.Project;
using Mladim.Domain.Models;
using MudBlazor;

namespace Mladim.Client.ViewModels;

public class ProjectVM
{
    public int Id { get; set; }
    public ProjectAttributesVM Attributes { get; private set; } = new ProjectAttributesVM();
    public DateRange DateRange { get; set; } = default!;
    public IEnumerable<NamedEntityVM> Staff { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Administration { get; set; } = new List<NamedEntityVM>();

    public IEnumerable<NamedEntityVM> Groups { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Partners { get; set; } = new List<NamedEntityVM>();
    public List<AttachedFileVM> Files { get; set; } = new List<AttachedFileVM>();

    public bool IsCompleted(DateTime dateTime) => this.DateRange.End < dateTime;
}
