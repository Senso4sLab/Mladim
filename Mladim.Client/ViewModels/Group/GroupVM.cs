using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;




public class GroupVM :NamedEntityVM
{   
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;    
    //public GroupType GroupType { get; set; }
    public IEnumerable<NamedEntityVM> Members { get; set; } = new List<NamedEntityVM>();
}