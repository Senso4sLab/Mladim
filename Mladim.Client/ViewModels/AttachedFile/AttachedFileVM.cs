namespace Mladim.Client.ViewModels.AttachedFile;

public class AttachedFileVM
{
    public string FileName { get; set; } = string.Empty;
    public List<byte> Data { get; set; } = new();
    public string ContentType { get; set; } = string.Empty;

    private AttachedFileVM()
    {
        
    }

    private AttachedFileVM(string fileName, List<byte> data, string contentType) =>
        (FileName, Data, ContentType) = (fileName, data, contentType);  
  

    public static AttachedFileVM Create(string fileName, IEnumerable<byte>data, string contentType) =>
        new AttachedFileVM(fileName, data.ToList(), contentType);
}
