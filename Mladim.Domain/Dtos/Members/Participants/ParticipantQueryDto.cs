﻿namespace Mladim.Domain.Dtos;

public class ParticipantQueryDto : ParticipantCommandDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
}