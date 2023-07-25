using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Features.Files.Queries;

public class FileResponse
{
    public MemoryStream Stream { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }

    private FileResponse(MemoryStream stream, string fileName, string contentType) =>
        (Stream,  ContentType, FileName) = (stream, contentType, fileName);
    

    public static FileResponse Create(MemoryStream stream, string fileName, string contentType) =>
        new FileResponse(stream, fileName, contentType);
}
