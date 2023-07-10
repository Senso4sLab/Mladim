namespace Mladim.Domain.Dtos;

public class ActivityWithProjectNameQueryDto : ActivityQueryDto
{
    public string ProjectName { get; set; } = string.Empty;
}
