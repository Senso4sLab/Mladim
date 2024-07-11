using Mladim.Domain.Enums;
using System.Runtime.CompilerServices;

namespace Mladim.Domain.Models;

public class ActivityWithProjectName : Activity
{  

    public NamedEntity Project { get; set; } = default!;

    private ActivityWithProjectName(int id, ActivityAttributes attibutes, DateTimeRange timeRange, int projectId, string projectName, List<ActivityGroup> groups, List<Participant> participant, List<AnonymousParticipantGroup> anonymousParticipantGroup) =>
        (Id, Attributes, TimeRange, Project, Groups, Participants, AnonymousParticipantGroups) = (id, attibutes, timeRange, NamedEntity.Create(projectId, projectName), groups, participant, anonymousParticipantGroup);


    public static ActivityWithProjectName Create(int projectId, string projectName, Activity activity) =>
       new ActivityWithProjectName(activity.Id, activity.Attributes, activity.TimeRange, projectId, projectName, new List<ActivityGroup>(), new List<Participant>(), new List<AnonymousParticipantGroup>());
    


    public static ActivityWithProjectName Create(int projectId, string projectName, Activity activity, IEnumerable<ActivityGroup> groups, IEnumerable<Participant> participant, IEnumerable<AnonymousParticipantGroup> anonymousParticipantGroup) =>
        new ActivityWithProjectName(activity.Id, activity.Attributes, activity.TimeRange, projectId, projectName, groups.ToList(), participant.ToList(), anonymousParticipantGroup.ToList());        
       
}

