using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class StaffMemberDetailsQueryDto : MemberDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int? YearOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;   
    public bool IsRegistered { get; set; }   
    
}