using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class MemberVM : NamedEntityVM
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public bool IsActive { get; set; } = true;
}
