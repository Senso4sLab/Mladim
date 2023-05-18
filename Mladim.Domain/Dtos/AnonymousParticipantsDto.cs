using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class AnonymousParticipantsDto
{
    public int Id { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }   
}
