using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos.Group;

public class UpdateGroupCommandDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    //public GroupType GroupType { get; set; }
    public List<int> Members { get; set; } = new();
}
