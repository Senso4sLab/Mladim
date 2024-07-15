using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.JSInterop;
using System.Globalization;

namespace Mladim.Client.Services.Csv;

public interface ICsvService
{

    void Open();
    void NextRow();
    MemoryStream Stream { get; }
    void Write(params string[] texts);
    void Write(IEnumerable<string> texts);
    void Close();
   
}
