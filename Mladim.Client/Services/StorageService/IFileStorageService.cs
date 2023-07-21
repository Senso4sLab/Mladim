namespace Mladim.Client.Services.StorageService;

public interface IFileStorageService
{
    Task DeleteFileAsync(string fileRouth, string containerName);
    Task<string> EditFileAsync(byte[] content, string extension, string containerName, string fileRoute);

    Task<string> SaveFileAsync(byte[] content, string extension, string containerName);

}
