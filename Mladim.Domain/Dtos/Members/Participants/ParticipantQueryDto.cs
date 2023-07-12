using Mladim.Domain.Contracts;

namespace Mladim.Domain.Dtos;

public class ParticipantQueryDto : INameableEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}