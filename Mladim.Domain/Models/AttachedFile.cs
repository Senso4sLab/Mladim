using System.Security.Cryptography;
namespace Mladim.Domain.Models;

public class AttachedFile : IEquatable<AttachedFile>
{
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FolderName { get; set; } = string.Empty;

    private AttachedFile() { }

    private AttachedFile(string fileName, string storedFileName, string contentType, string folderName) =>
        (FileName, StoredFileName, ContentType, FolderName) = (fileName, storedFileName, contentType, folderName);
    
   

    public static AttachedFile Create(string fileName, string storedFileName, string contentType, string folderName) =>
        new AttachedFile(fileName, storedFileName, contentType, folderName);

    public bool Equals(AttachedFile? other) =>
        other is AttachedFile af && af.FileName == this.FileName;

    public override bool Equals(object? obj) =>
        obj is AttachedFile attachedFile && Equals(attachedFile);    

    public override int GetHashCode() =>
        this.FileName.GetHashCode();
    

}
