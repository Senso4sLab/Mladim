using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class GroupQueryDto 
{
    public int Id { get; set; }
    public GroupType GroupType { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    private GroupQueryDto(int id,  string name, string description, GroupType groupType)
    {
        this.Id = id;
        this.FullName = name;
        this.GroupType = groupType;
        this.Description = description;
    }

    public static GroupQueryDto Create(int id, string name, string description, GroupType groupType) =>
        new GroupQueryDto(id, name, description, groupType);


}
