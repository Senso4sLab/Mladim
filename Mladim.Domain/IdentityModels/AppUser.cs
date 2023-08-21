
using Mladim.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Mladim.Domain.IdentityModels;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public List<Organization> Organizations { get; set; } = new();



    private AppUser()
    {
        
    }

    private AppUser(string name, string surname, string nickname, string username, string email) =>
        (this.Name, this.Surname, this.Nickname, this.UserName, this.Email) = (name, surname, nickname, username, email);

    public static AppUser Create(string name, string surname, string nickname, string username, string email) =>
        new AppUser(name, surname, nickname, username, email);
        
}
