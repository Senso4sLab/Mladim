namespace Mladim.Domain.Dtos;

public class StaffMemberCommandDto
{
    public int Id { get; set; }
    public bool IsLead { get; set; }

    private StaffMemberCommandDto(int id, bool isLead) => 
        (Id, IsLead) = (id, isLead);
    public static StaffMemberCommandDto Create(int id, bool isLead = false) =>
        new StaffMemberCommandDto(id, false);
    

}
