namespace Mladim.Domain.Dtos.AttachedFile;



public class AttachedFileQueryDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
}



public class AttachedFileCommandDto
{
    public string FileName { get; set; } = string.Empty;
    public List<byte> Data { get; set; } = new ();
    public string ContentType { get; set; } = string.Empty;


    public AttachedFileCommandDto()
    {
        
    }

    private AttachedFileCommandDto(string fileName, List<byte> data, string contentType) =>
        (FileName, Data, ContentType) = (fileName, data, contentType);
 
       

    public static AttachedFileCommandDto Create(string fileName, List<byte>data, string contentType) =>
        new AttachedFileCommandDto (fileName, data, contentType);

}
