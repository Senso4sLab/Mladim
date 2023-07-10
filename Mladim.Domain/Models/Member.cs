using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Member : BaseEntity<int>
{
    public string FullName { get; set; } = string.Empty;    
    public bool IsActive { get; set; } = true;

    protected Member()
    {
        
    }

    protected Member(int id)
    {
        this.Id = id;
    }
    protected Member(string fullName, bool isActive)
    {       
        this.FullName = fullName;            
        this.IsActive = isActive;
    }

    public static Member Create(string fullName, bool isActive) =>
        new Member(fullName, isActive);


}

