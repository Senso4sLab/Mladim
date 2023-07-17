using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class GroupVM 
{
    public int Id { get; set; }    
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;    
    public GroupType GroupType { get; set; }
    public List<NamedEntityVM> Members { get; set; } = new();
}