namespace Mladim.Client.Models;

public class DoughnutPiece
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string Text { get; set; }
    public string Fill { get; set; }


    public DoughnutPiece()
    {
        
    }

    private DoughnutPiece(string name, int value, string text, string fill) => 
        (this.Name, this.Value, this.Text, this.Fill) = (name, value, text, fill);   

    public static DoughnutPiece Create(string name, int value, string text, string fill = null) => 
        new DoughnutPiece(name, value, text, fill);
}
