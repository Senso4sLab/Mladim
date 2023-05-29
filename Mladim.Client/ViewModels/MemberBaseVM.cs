namespace Mladim.Client.ViewModels;

public class MemberBaseVM
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj) =>    
         obj is MemberBaseVM mb && 
         mb.Id == this.Id &&
         mb.Name == this.Name;    


    public override int GetHashCode() =>
        HashCode.Combine(this.Id, this.Name);
    

}
