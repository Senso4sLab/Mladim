using Mladim.Domain.Dtos;
using MudBlazor;

namespace Mladim.Client.ViewModels;

public class ProjectVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? WebpageUrl { get; set; }

   
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime End { get; set; } = DateTime.Now;
    
    public List<StaffMemberSubjectVM> Staff { get; set; } = new();


    private DateRange projectDateRange;
    public DateRange ProjectDateRange
    { 
        get => projectDateRange ??= new DateRange(Start, End);
        set
        {
            projectDateRange = value;
            this.Start = projectDateRange.Start.Value;
            this.End = projectDateRange.End.Value;          
        }
    }      
       

    private IEnumerable<MemberBaseVM> leadStaff;
    public IEnumerable<MemberBaseVM> LeadStaff
    {
        get => leadStaff ??= Staff.Where(smp => smp.IsLead)
                                  .Select(smp => new MemberBaseVM { Id = smp.StaffMemberId, Name = smp.Name })
                                  .ToList();

        set => leadStaff = value;       
    }


    private IEnumerable<MemberBaseVM> administrators;
    public IEnumerable<MemberBaseVM> Administrators
    {
        get => administrators ??= Staff.Where(smp => !smp.IsLead)
                                  .Select(smp => new MemberBaseVM { Id = smp.StaffMemberId, Name = smp.Name })
                                  .ToList();

        set => administrators = value;
    }
  
        
    public List<GroupBaseVM> Groups { get; set; } = new();
    public IEnumerable<MemberBaseVM> Partners { get; set; } = new List<MemberBaseVM>();
}
