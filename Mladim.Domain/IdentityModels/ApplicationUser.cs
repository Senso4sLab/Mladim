
using Mladim.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Mladim.Domain.IdentityModels;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Nickname { get; set; }
    public List<Organization> Organizations { get; set; } = new();
}
