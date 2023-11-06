using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos.Members.AnonymousParticipants;

public class AnonymousParticipantCommandDto
{   
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
}
