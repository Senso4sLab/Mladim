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
    public AgeGroups AgeGroup => 
        AgeClassification(this.Age);
    public List<Activity> Activities { get; set; } = new ();
    private Participant() { }
    internal Participant(int id) : base(id) { }


    private AgeGroups AgeClassification(int age) => age switch
    {
        <= 14 => AgeGroups.Age12_14,
        <= 19 => AgeGroups.Age15_19,
        <= 24 => AgeGroups.Age20_24,
        <= 29 => AgeGroups.Age25_29,
        _ => AgeGroups.Age30_35,
    };

}
