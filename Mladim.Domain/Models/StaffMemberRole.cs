using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class StaffMemberRole
{
    public bool IsLead { get; private set; }
    public StaffMember StaffMember { get; private set; }
    private StaffMemberRole(StaffMember staffMember, bool islead) =>
        (StaffMember, IsLead) =(staffMember, islead);   

    public static StaffMemberRole Create(int staffMemberId, bool isLead = false) =>
        new StaffMemberRole((StaffMember)Member.Create(Enums.GroupType.StaffMember, staffMemberId), isLead);

}
