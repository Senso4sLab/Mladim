namespace Mladim.Client.Models;

public class DoughnutPiece
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }


    public DoughnutPiece()
    {
        
    }

    private DoughnutPiece(string name, int value) => 
        (this.Name, this.Value) = (name, value);   

    public static DoughnutPiece Create(string name, int value) => 
        new DoughnutPiece(name, value);
}
