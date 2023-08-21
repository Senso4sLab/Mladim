namespace Mladim.Client.ViewModels.Activity;

public class ActivityForGantt
{
    public int Id { get; set; }
    public int ActivityId { get; set; }
    public string ActivityName { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? ParentId { get; set; }

    public ActivityForGantt()
    {

    }

    private ActivityForGantt(int id, int activityId, string activityName, NamedEntityVM project, DateTimeRangeVM dateTimeRange) =>
        (Id, ActivityId, ActivityName, ProjectId, ProjectName, StartDate, EndDate) =
            (id, activityId, activityName, project.Id, project.FullName, dateTimeRange.StartDate, dateTimeRange.EndDate);



    public static ActivityForGantt Create(int id, int activityId, string activityName, NamedEntityVM project, DateTimeRangeVM dateTimeRange) =>
        new ActivityForGantt(id, activityId, activityName, project, dateTimeRange);

}
