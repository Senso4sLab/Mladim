namespace Mladim.Client.Models;

public interface IExportChart
{
    string StackedBarWidth { get; set; }
    void AddExportChart(Func<Task> exportChart);
    void RemoveExportChart(Func<Task> exportChart);
   
}
