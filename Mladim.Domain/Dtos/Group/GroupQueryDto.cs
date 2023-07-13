using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class GroupQueryDto 
{
    public int Id { get; set; }
    public GroupType GroupType { get; set; }
    public string Name { get; set; } = string.Empty;    

    private GroupQueryDto(int id,  string name, GroupType groupType)
    {
        this.Id = id;
        this.Name = name;
        this.GroupType = groupType;
    }

    public static GroupQueryDto Create(int id, string name, GroupType groupType) =>
        new GroupQueryDto(id, name, groupType);


}
