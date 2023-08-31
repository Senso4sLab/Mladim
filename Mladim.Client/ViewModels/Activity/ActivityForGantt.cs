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
    public string Duration { get; set; }
    public int? ParentId { get; set; }

    public ActivityForGantt()
    {

    }

    private ActivityForGantt(int id, int activityId, string activityName, NamedEntityVM project, DateTimeRangeVM dateTimeRange)
    {

        this.Id = id;
        this.ActivityId = activityId;
        this.ActivityName = activityName;
        this.ProjectId = project.Id;
        this.ProjectName = project.FullName;
        this.StartDate = dateTimeRange.StartDate;
          
        this.EndDate = dateTimeRange.EndDate;
        this.Duration = (this.EndDate - this.StartDate).Days.ToString();
    }



    public static ActivityForGantt Create(int id, int activityId, string activityName, NamedEntityVM project, DateTimeRangeVM dateTimeRange) =>
        new ActivityForGantt(id, activityId, activityName, project, dateTimeRange);

}
