

namespace Mladim.Domain.Models;




public class Partner : NamedEntity
{
 
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }    
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public List<Activity> Activities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();   
    public int OrganizationId { get; set; }

    public Partner()
    {
        
    }
    //private Partner() {}    
    private Partner(int id) => Id = id;
    public static Partner Create(int parnetId) =>
        new Partner(parnetId);  

}
