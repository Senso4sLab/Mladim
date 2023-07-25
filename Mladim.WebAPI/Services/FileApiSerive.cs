using Azure.Core;
using Mladim.Application.Contracts.File;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Services;

public class FileApiSerive : IFileApiService
{
    private IWebHostEnvironment Env { get; set; }
    public FileApiSerive(IWebHostEnvironment env) => Env = env;    
       
    public async Task<string> AddFileAsync(byte[] file, string folder, string fileName)
    {
       
        string filePath = Path.Combine(Env.WebRootPath, "Files", folder);
        
        string trustedFileName = $"{Path.GetRandomFileName()}.{Path.GetExtension(fileName)}";
        string trustedPath = Path.Combine(filePath, trustedFileName);
        
        using (var stream = File.OpenWrite(trustedPath))
        {
            stream.Seek(0, SeekOrigin.Begin);
            await stream.WriteAsync(file, 0, file.Length);
        }
        return trustedFileName;        
    }

    public bool DeleteFile(string trustedFileName, string folder)
    {
        string filePath = Path.Combine(Env.WebRootPath, "Files", folder);
        string trustedPath = Path.Combine(filePath, trustedFileName);

        if (File.Exists(trustedPath))
        {
            File.Delete(trustedPath);
            return true;
        }
        return false;
    }
}
