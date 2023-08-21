namespace Mladim.Client.ViewModels;

public class NamedEntityVM : IEquatable<NamedEntityVM>
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;

    public bool Equals(NamedEntityVM? other) =>
        other is not null && other.Id == this.Id;

    public override bool Equals(object? obj) =>
        obj is NamedEntityVM entity ? Equals(entity) : false;   
   


    public override int GetHashCode() =>
        HashCode.Combine(Id);


    public static NamedEntityVM Create(int id, string name) =>
        new NamedEntityVM
        {
            Id = id,
            FullName = name
        };

}
