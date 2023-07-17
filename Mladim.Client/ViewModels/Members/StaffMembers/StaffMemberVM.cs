﻿namespace Mladim.Client.ViewModels;

public class StaffMemberVM : MemberVM
{
    public int? YearOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;    
    public bool IsRegistered { get; set; }
}
