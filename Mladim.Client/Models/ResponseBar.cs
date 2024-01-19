namespace Mladim.Client.Models;

public class ResponseBar
{
    public string XName { get; set; }
    public string YName { get; set; }
    public string Fill { get; set; }
    public double LeftRadius { get; set; }
    public double RightRadius { get; set; }   

    public IEnumerable<BarValue> Data { get; } = new List<BarValue>();

    private ResponseBar(string xName, string yName, string fill, IEnumerable<BarValue> data)
    {
        this.XName = xName;
        this.YName = yName;
        this.Fill = fill;
        this.Data = data.ToList();
    }
    public static ResponseBar CreateResponseBar(string fill, IEnumerable<BarValue> data) =>
        new ResponseBar(nameof(BarValue.ChartName), nameof(BarValue.Value), fill, data);
}


public class BarValue
{
    public string ChartName { get; }
    public string Name { get; }
    public float Value { get; }
    public string Label => $"{this.Name} {this.Value}%";
    public string ClassIcon { get; }

    private BarValue(string chartName, string name, float value, string classIcon)
    {
        this.ChartName = chartName;
        this.Name = name;
        this.Value = value;
        this.ClassIcon = classIcon;
    }

    public static BarValue CreateBarValue(string name, float value, string classicon = "") =>
        new BarValue("barValue", name, value, classicon);

}