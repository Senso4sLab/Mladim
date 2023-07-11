using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Member : BaseEntity<int>
{
    public virtual string FullName { get; }
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

    


}

