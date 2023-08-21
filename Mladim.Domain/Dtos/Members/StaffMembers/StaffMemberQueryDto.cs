
using Mladim.Domain.Dtos.Members;


namespace Mladim.Domain.Dtos;

public class StaffMemberQueryDto : NamedEntityDto
{   
    public bool IsLead { get; set; }
   
}

public class StaffMemberLeadQueryDto : NamedEntityDto
{
    public List<int> ProjectIds { get; set; } = new List<int>();
    public List<int> ActivityIds { get; set; } = new List<int>();

    public StaffMemberLeadQueryDto()
    {
        
    }

    public static StaffMemberLeadQueryDto Create(int id, string fullName, IEnumerable<int> projectIds, IEnumerable<int> activityIds)
    {
        return new StaffMemberLeadQueryDto
        {
            Id = id,
            FullName = fullName,
            ProjectIds = projectIds.ToList(),
            ActivityIds = activityIds.ToList(),
        };
    }


}
