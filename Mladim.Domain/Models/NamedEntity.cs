
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class NamedEntity : BaseEntity<int>
{
    public virtual string FullName { get; set; } = string.Empty;
   
}
