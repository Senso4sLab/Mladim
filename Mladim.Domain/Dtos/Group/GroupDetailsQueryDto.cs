using Mladim.Domain.Enums;
using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Dtos;

public class GroupDetailsQueryDto
{
    public int Id { get; set; }

    public MemberType GroupType { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public string Description { get; set; }
    public List<MemberDto> Members { get; set; }
}



