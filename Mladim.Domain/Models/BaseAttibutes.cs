namespace Mladim.Domain.Models;

public abstract class BaseAttibutes
{
    public string Name { get; protected set; } = string.Empty;
    public string Description { get; protected set; } = string.Empty;
}
