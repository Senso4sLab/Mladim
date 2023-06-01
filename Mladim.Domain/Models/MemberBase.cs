using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class MemberBase
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
