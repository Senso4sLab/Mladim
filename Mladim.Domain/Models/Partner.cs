using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Partner : BaseEntity<int>
{    
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;    
    public List<Activity> Activities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();


    private Partner()
    {
        
    }


    private Partner(int id)
    {
       this.Id = id;
    }

    public static Partner Create(int parnetId) =>
        new Partner(parnetId);
}
