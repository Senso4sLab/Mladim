﻿using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class MemberDto : BaseMemberDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; } = true;
    public int Year { get; set; }
}

