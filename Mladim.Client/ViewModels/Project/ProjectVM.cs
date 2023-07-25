using Mladim.Client.ViewModels.AttachedFile;
using Mladim.Client.ViewModels.Project;

using MudBlazor;

namespace Mladim.Client.ViewModels;

public class ProjectVM
{
    public int Id { get; set; }
    public ProjectAttributesVM Attributes { get; private set; } = new ProjectAttributesVM();    
    public DateRange DateRange { get; set; } = new DateRange();

    public IEnumerable<NamedEntityVM> Staff { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Administration { get; set; } = new List<NamedEntityVM>();

    public IEnumerable<NamedEntityVM> Groups { get; set; } = new List<NamedEntityVM>();
    public IEnumerable<NamedEntityVM> Partners { get; set; } = new List<NamedEntityVM>();
    public List<AttachedFileVM> Files { get; set; } = new List<AttachedFileVM>();    


}
