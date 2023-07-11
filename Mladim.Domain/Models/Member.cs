using Mladim.Domain.Contracts;
using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;




public class Member : BaseEntity<int>, IFullName
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public bool IsActive { get; set; } = true;
    public int OrganizationId { get; set; }
    public string FullName => $"{this.Name} {this.Surname}";
    protected Member() { }
    protected Member(int id) => this.Id = id;
    public static Member Create(MemberType memberType, int id) =>
      memberType switch
      {
          MemberType.StaffMember => new StaffMember(id),
          MemberType.Participant => new Participant(id),
          _ => throw new NotImplementedException()
      };

}

