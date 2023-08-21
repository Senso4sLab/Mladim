
using Mladim.Domain.Dtos.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;





public class NamedEntity : BaseEntity<int>
{
    public virtual string FullName { get; set; } = string.Empty;



    public static NamedEntity Create(int id, string fullName) => 
        new NamedEntity
        { 
            Id = id,
            FullName = fullName 
        };
}


public class StaffMemberLeadQuery : NamedEntity
{
    public List<int> ProjectIds { get; set; } = new List<int>();
    public List<int> ActivityIds { get; set; } = new List<int>();

    private StaffMemberLeadQuery()
    {

    }

    public static StaffMemberLeadQuery Create(int id, string fullName, IEnumerable<int> projectIds, IEnumerable<int> activityIds)
    {
        return new StaffMemberLeadQuery
        {
            Id = id,          
            FullName = fullName,
            ProjectIds = projectIds.ToList(),
            ActivityIds = activityIds.ToList(),
        };
    }


}
