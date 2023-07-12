
using Mladim.Domain.Dtos.Members;


namespace Mladim.Domain.Dtos;

public class StaffMemberQueryDto : NamedEntityDto
{   
    public bool IsLead { get; set; }
   
}
