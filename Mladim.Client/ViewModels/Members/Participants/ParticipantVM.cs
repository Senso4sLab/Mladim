

using Mladim.Domain.Enums;

namespace Mladim.Client.ViewModels;

public class ParticipantVM : MemberVM
{
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }
}


