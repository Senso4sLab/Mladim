using Mladim.Domain.Enums;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class MemberDto 
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;   
}


