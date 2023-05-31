﻿using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class UpdateParticipantCommandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public bool IsActive { get; set; } = true;
}
