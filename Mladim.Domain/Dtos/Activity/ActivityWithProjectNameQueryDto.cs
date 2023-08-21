using Mladim.Domain.Dtos.Members;

namespace Mladim.Domain.Dtos;

public class ActivityWithProjectNameQueryDto : ActivityQueryDto
{
    public NamedEntityDto Project { get; set; } = default!;
}
