namespace Mladim.Client.Models;

public class DoughnutPiece
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string Text { get; set; }


    public DoughnutPiece()
    {
        
    }

    private DoughnutPiece(string name, int value, string text) => 
        (this.Name, this.Value, this.Text) = (name, value, text);   

    public static DoughnutPiece Create(string name, int value, string text) => 
        new DoughnutPiece(name, value, text);
}
