using Mladim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;

public class Participant : Member
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int Age { get; set; }
    public AgeGroups AgeGroup { get; set; }
    public Gender Gender { get; set; }
    public List<Activity> Activities { get; set; } = new ();

    public override string FullName => $"{this.Name} {this.Surname}";

    private Participant()
    {

    }

    private Participant(int id):base(id)
    {
       
    }
    

    private Participant(int id, string name, string surname, Gender gender, int age, AgeGroups ageGroups, bool isActive) : base($"{name} {surname}", isActive)
    {
       
        this.Name = name;
        this.Surname = surname;
        this.Age = age;
        this.Gender = gender;
        this.AgeGroup = ageGroups;
    }


    public static Participant Create(int id) =>
        new Participant(id);


    public static Participant Create(string name, string surname, Gender gender, int age, AgeGroups ageGroups) =>
        new Participant(0, name, surname, gender, age, ageGroups, true);

    public static Participant Create(int id, string name, string surname, Gender gender, int age, AgeGroups ageGroups, bool isActive) =>
       new Participant(id, name, surname, gender, age, ageGroups, isActive);


}
