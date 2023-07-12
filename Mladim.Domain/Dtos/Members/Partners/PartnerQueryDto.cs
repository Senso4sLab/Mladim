using Mladim.Domain.Contracts;
using Mladim.Domain.Models;

namespace Mladim.Domain.Dtos;

public class PartnerQueryDto : INameableEntity
{
    public int Id { get; set; }

    public string FullName { get;  set; } = string.Empty;
}
