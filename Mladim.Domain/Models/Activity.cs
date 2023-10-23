using Mladim.Domain.Enums;
using Mladim.Domain.Models.Survey.Questions;
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

    public List<AnonymousParticipantGroup> AnonymousParticipantGroups { get; set; } = new();

    public void Add(AnonymousParticipantGroup apg)
    {
        var anonymousParticipant = this.AnonymousParticipantGroups.FirstOrDefault(apg => apg.AnonymousParticipant == apg.AnonymousParticipant);

        if (anonymousParticipant != null)
            anonymousParticipant.Number += apg.Number;
        else
            this.AnonymousParticipantGroups.Add(apg);
    }

    public int? SurveyQuestionnairyId { get;set; }
    public SurveyQuestionnairy SurveyQuestionnairy { get; set; } = default!;

    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;
    
}


