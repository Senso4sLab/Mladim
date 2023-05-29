using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class MemberDetailsDto : MemberBase
{    
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; } = true;
    public int Year { get; set; }
}

