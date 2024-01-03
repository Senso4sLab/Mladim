using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
using Mladim.Domain.Models.Survey.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;



public class Activity : BaseEntity<int>
{
    public ActivityAttributes Attributes { get; protected set; } = default!;
    public DateTimeRange TimeRange { get; protected set; } = default!;

    protected Activity() { }  

    public List<StaffMemberActivity> Staff { get; set; } = new();
    public List<Partner> Partners { get; set; } = new();
    public List<Participant> Participants { get; set; } = new();
    public List<ActivityGroup> Groups { get; set; } = new();
    public List<AttachedFile> Files { get; set; } = new();

    public void Add(ActivityGroup group) =>
       this.Groups.Add(group);

    public void ChangeName(string name)
    {
        var attributes = this.Attributes.Clone();
        attributes.ChangeName(name);
        this.Attributes = attributes;        
    }

    public void OffsetDateTimeRangeForRepetitiveInterval()
    {
        var datetimeRange = this.TimeRange.Clone();
        datetimeRange.OffsetDateTimeForRepetitiveInterval(Attributes.RepetitiveInterval);       
        this.TimeRange = datetimeRange; 
    }

   



    public List<AnonymousParticipantGroup> AnonymousParticipantGroups { get; set; } = new();

    public void Add(AnonymousParticipantGroup apg)
    {
        var anonymousParticipant = this.AnonymousParticipantGroups.FirstOrDefault(apg => apg.AnonymousParticipant == apg.AnonymousParticipant);

        if (anonymousParticipant != null)
            anonymousParticipant.Number += apg.Number;
        else
            this.AnonymousParticipantGroups.Add(apg);
    }

    public Activity Clone()
    {
        return this.MemberwiseClone() as Activity;
    }

    public int? SurveyQuestionnairyId { get;set; }
    public SurveyQuestionnairy SurveyQuestionnairy { get; set; } = default!;
    public List<AnonymousSurveyResponse> AnonymousSurveyResponses { get; set; } = new();

    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    
}


