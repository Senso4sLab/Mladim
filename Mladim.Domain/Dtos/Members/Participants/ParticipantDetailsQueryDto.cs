using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ParticipantDetailsQueryDto : MemberDto
{
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }
}
