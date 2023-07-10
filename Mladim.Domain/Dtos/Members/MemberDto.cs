using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class MemberDto 
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;   
    public bool IsActive { get; set; } = true;   
}


