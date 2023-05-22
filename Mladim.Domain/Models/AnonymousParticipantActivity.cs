namespace Mladim.Domain.Models;

public class AnonymousParticipantActivity
{   
    public int Number { get; set; }
    public int ActivityId { get; set; }
    public Activity Activity { get; set; }
    public int AnonymousParticipantId { get; set; }
    public AnonymousParticipant AnonymousParticipant { get; set; }
}
