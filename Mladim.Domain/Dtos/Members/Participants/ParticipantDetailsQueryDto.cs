using Mladim.Domain.Dtos.Members;
using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class ParticipantDetailsQueryDto : NamedEntityDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public bool IsActive { get; set; } = true;
    public int OrganizationId { get; set; }   
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }
}
