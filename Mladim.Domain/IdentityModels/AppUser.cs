
using Mladim.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Mladim.Domain.IdentityModels;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public List<Organization> Organizations { get; set; } = new();
}
