using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Partner : Member
{   
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? WebpageUrl { get; set; }
    public string? ContactPerson { get; set; }    
    public string? PhoneNumber { get; set; }    

    public List<Activity> Activities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();


    private Partner()
    {
        
    }


    private Partner(int id) : base()
    {
       
    }

    private Partner(int id, string name, string? email, string? description, string? webPageUrl, string? contactPerson, string? phoneNumber, bool isActive) : base(name, isActive)
    {
        this.Id = id;       
        this.Email = email;
        this.Description = description;
        this.WebpageUrl = webPageUrl;
        this.ContactPerson = contactPerson;
        this.PhoneNumber = phoneNumber;
     
    }        

    public static Partner Create(int parnetId) =>
        new Partner(parnetId);

    public static Partner Create(string name, string? email, string? description, string? webPageUrl, string? contactPerson, string? phoneNumber) =>
        new Partner(0,name, email, description,webPageUrl, contactPerson, phoneNumber, true);

    public static Partner Create(int id, string name, string? email, string? description, string? webPageUrl, string? contactPerson, string? phoneNumber, bool isActive) =>
        new Partner(id, name, email, description, webPageUrl, contactPerson, phoneNumber, isActive);

}
