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

    private StaffMemberRole(StaffMember staffMember, bool islead = false)
    {
        this.StaffMember = staffMember;
        this.IsLead = islead;
    }

    public static StaffMemberRole Create(int staffMemberId, bool isLead = false) =>
        new StaffMemberRole(StaffMember.Create(staffMemberId), isLead);

}
