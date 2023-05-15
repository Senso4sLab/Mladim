using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class StaffMember : Member
{
    public string Email { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRegistered { get; set; }   
    
}