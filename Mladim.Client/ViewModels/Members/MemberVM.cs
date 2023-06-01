using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class MemberVM : MemberBaseVM
{   
   
    public Gender Gender { get; set; }   
    public bool IsActive { get; set; } = true;
}
