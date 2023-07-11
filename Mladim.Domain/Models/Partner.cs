using Microsoft.AspNetCore.Identity;
using Mladim.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Partner : BaseEntity<int>, IFullName
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }    
    public string? PhoneNumber { get; set; } 
    public List<Activity> Activities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();   
    private Partner() { }    
    private Partner(int id) : base() { } 
    public static Partner Create(int parnetId) =>
        new Partner(parnetId);  

}
