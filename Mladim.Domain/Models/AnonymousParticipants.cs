using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class AnonymousParticipants
{
    public int Id { get; set; }
    public Gender Gender { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public int Number { get; set; }

    public int ActivityId { get; set; }
    public ActivityDto Activity { get; set; }

    public override bool Equals(object? obj) =>
       obj is AnonymousParticipants ap ? this.Equals(ap) : false;

    private bool Equals(AnonymousParticipants ap) =>
        ap.AgeGroup == this.AgeGroup && ap.Gender == this.Gender;
    public override int GetHashCode() =>
        HashCode.Combine(AgeGroup, Gender);
}
