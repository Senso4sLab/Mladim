using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class StaffMember : Member
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int? YearOfBirth { get; set; }
    public bool IsRegistered { get; set; }
    public Gender Gender { get; set; }
    public List<StaffMemberActivity> StaffActivities { get; set; } = new();
    public List<StaffMemberProject> StaffProjects { get; set; } = new();   
    

    private StaffMember()
    {

    }

    private StaffMember(int id) : base(id)
    {
      
    }   

    private StaffMember(int id, string name, string surname, Gender gender, string email, int? yearOfBirth, bool isRegistered) : base($"{name} {surname}", true)
    {
        this.Id = id;
        this.Name = name;
        this.Surname = surname;
        this.Gender = gender;
        this.Email = email;
        this.YearOfBirth = yearOfBirth;
        this.IsRegistered = isRegistered;
    }

    public static StaffMember Create(int id) => 
        new StaffMember(id);

    public static StaffMember Create(string name, string surname, Gender gender, string email, int? yearOfBirth) =>
        new StaffMember(0, name, surname, gender, email, yearOfBirth, false);

    public static StaffMember Create(int id, string name, string surname, Gender gender, string email, int? yearOfBirth, bool isRegisterd) =>
        new StaffMember(id, name, surname, gender, email, yearOfBirth, isRegisterd);

}