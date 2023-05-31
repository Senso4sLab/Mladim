using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class StaffMemberDetailsQueryDto : MemberDetailsDto
{
    public int? YearOfBirth { get; set; }
    public string Email { get; set; }
   
    public bool IsRegistered { get; set; }   
    
}