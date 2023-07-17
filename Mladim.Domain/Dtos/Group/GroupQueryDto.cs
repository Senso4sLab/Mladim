using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class GroupQueryDto 
{
    public int Id { get; set; }
    public GroupType GroupType { get; set; }
    public string FullName { get; set; } = string.Empty;
    private GroupQueryDto(int id,  string name, GroupType groupType)
    {
        this.Id = id;
        this.FullName = name;
        this.GroupType = groupType;
    }

    public static GroupQueryDto Create(int id, string name, GroupType groupType) =>
        new GroupQueryDto(id, name, groupType);


}
