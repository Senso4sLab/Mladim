﻿using Mladim.Domain.Enums;

namespace Mladim.Domain.Dtos;

public class UpdateStaffMemberCommandDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public int Year { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }
    public string Email { get; set; }
}