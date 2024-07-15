using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.JSInterop;
using Mladim.Client.Services.Csv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Client.Csv;

public class CsvService : ICsvService
{

    private MemoryStream MemoryStream { get; set; } = default!;
    private StreamWriter StreamWriter { get; set; } = default!;
    private CsvWriter CsvWriter { get; set; } = default!;
    public MemoryStream Stream
    {
        get
        {
            MemoryStream.Position = 0;
            return MemoryStream; 
        }       
    }

    public void NextRow()
    {
        CsvWriter.NextRecord();
    }
    public void Open()
    {
        MemoryStream = new MemoryStream();
        StreamWriter = new StreamWriter(MemoryStream);
        CsvWriter = new CsvWriter(StreamWriter, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", LeaveOpen = true, });
    }

    public void Write(params string[] texts)
    {
        foreach (string text in texts) 
            CsvWriter.WriteField(text);
    }

    public void Close()
    {
        CsvWriter?.Dispose();
    }

    public void Write(IEnumerable<string> texts)
    {
        foreach (string text in texts)
            CsvWriter.WriteField(text);
    }
}
