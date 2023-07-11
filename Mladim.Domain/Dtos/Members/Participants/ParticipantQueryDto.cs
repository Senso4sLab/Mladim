using Mladim.Domain.Contracts;

namespace Mladim.Domain.Dtos;

public class ParticipantQueryDto : IFullName
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}