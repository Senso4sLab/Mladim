namespace Mladim.Domain.Dtos;

public class ParticipantQueryDto : MemberDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}