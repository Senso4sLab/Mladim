using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Participant : Member
{
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }   
    public List<Activity> Activities { get; set; } = new ();
}
