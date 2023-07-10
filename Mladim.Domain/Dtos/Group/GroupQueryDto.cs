namespace Mladim.Domain.Dtos;

public class GroupQueryDto 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    private GroupQueryDto(int id,  string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public static GroupQueryDto Create(int id, string name) =>
        new GroupQueryDto(id, name);


}
