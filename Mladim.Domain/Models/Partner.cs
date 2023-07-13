

namespace Mladim.Domain.Models;

public class Partner : NamedEntity
{    
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }    
    public string? PhoneNumber { get; set; } 
    public List<Activity> Activities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();   
    public int OrganizationId { get; set; }
    private Partner() {}    
    private Partner(int id) : base() { } 
    public static Partner Create(int parnetId) =>
        new Partner(parnetId);  

}
